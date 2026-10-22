using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

public enum EnemyState {
    Patrol,
    Search,
    Chase,
    Catch
}

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Human player;
    RayCast_Enemy rayEnemy;
    private EnemyState currentState;
    float caughtDistance = 0.8f;
    [SerializeField] private float searchDuration = 5.0f;
    float searchTimer = 0.0f;
    Patrol patrol;
    Patrol2 patrol2;
    Animator animator;
    VisionSensor sensor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Model").GetComponent<Human>();
        currentState = EnemyState.Patrol;
        rayEnemy = GetComponentInChildren<RayCast_Enemy>();
        patrol2 = GetComponent<Patrol2>();
        sensor = GetComponentInChildren<VisionSensor>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) <= caughtDistance)
        {
            ChangeState(EnemyState.Catch);
        }

        switch (currentState)
        {
            case EnemyState.Patrol:
                UpdatePatrol();
                break;
            case EnemyState.Search:
                UpdateSearch();
                break;
            case EnemyState.Chase:
                UpdateChase();
                break;
            case EnemyState.Catch:
                UpdateCatch();
                break;
        }
    }

    void UpdatePatrol()
    {
        Debug.Log("UpdatePatrol");

        if (rayEnemy.playerHit)
        {
            ChangeState(EnemyState.Chase);
        }

        patrol2.ExecutePatrol();
    }

    void UpdateSearch()
    {
        Debug.Log("UpdateSearch");
        if (rayEnemy.playerHit)
            ChangeState(EnemyState.Chase);
        else
        {
            searchTimer += Time.deltaTime;
            if (searchTimer >= searchDuration)
            {
                ChangeState(EnemyState.Patrol);
            }

            sensor.ScanEnvironment();
            Search();
        }
    }

    void UpdateChase()
    {
        if (!rayEnemy.playerHit && IsDestinationReached())
        {
            ChangeState(EnemyState.Search);
        }

        sensor.ScanEnvironment();
        Chase();
    }

    void UpdateCatch()
    {
        Debug.Log("UpdateCatch");
        Time.timeScale = 0f;
    }

    void ChangeState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Patrol:
                agent.speed = 2.0f;
                SetPoint();
                break;

            case EnemyState.Search:
                searchTimer = 0.0f;
                agent.speed = 2.0f;
                break;

            case EnemyState.Chase:
                agent.speed = 2.2f;
                break;
        }
    }

    bool IsDestinationReached()
    {
        // 経路計算中ではない
        if (!agent.pathPending)
        {
            // agent.remainingDistance目的地までの距離
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Debug.Log("目的地に到着しました");
                    return true;
                }
            }
        }
        return false;
    }

    void SetPoint()
    {
        float min_distance = 500.0f;
        int point = 0;
        for (int i = 0; i < 9; i++) {
            float distance = Vector3.Distance(transform.position, patrol.points[i].position);
            if (min_distance > distance)
            {
                min_distance = distance;
                point = i;
            }
        }
        patrol.destPoint = point;
    }

    void Search()
    {
        agent.transform.Rotate(0, 90 * Time.deltaTime, 0);
        animator.SetBool("isRun", false);
        animator.SetBool("isWalk", false);
    }

    void Chase()
    {
        Debug.Log("UpdateChase");
        Debug.Log(agent.speed);
        agent.destination = rayEnemy.playerLastPosition;
        Debug.Log(agent.destination);

        int x = Mathf.FloorToInt(rayEnemy.playerLastPosition.x);
        int z = Mathf.FloorToInt(rayEnemy.playerLastPosition.z);
        float y = 2.5f;
        rayEnemy.playerLastPosition = new Vector3(x + 0.5f, y, z + 0.5f);

        animator.SetBool("isRun", true);
    }

}
