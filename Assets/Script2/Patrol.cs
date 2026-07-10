using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

    public Transform[] points;
    public int destPoint = 0;
    private NavMeshAgent agent;
    Animator animator;
    RayCast_Enemy rayEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rayEnemy = GetComponentInChildren<RayCast_Enemy>();

        // autoBraking を無効にすると、目標地点の間を継続的に移動します
        //(つまり、エージェントは目標地点に近づいても
        // 速度をおとしません)
        agent.autoBraking = false;
    }

    public void ExecutePatrol()
    {
        // 地点がなにも設定されていないときに返します
        if (points.Length == 0)
            return;

        animator.SetBool("isRun", false);
        animator.SetBool("isWalk", true);
        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = points[destPoint].position;

        // 配列内の次の位置を目標地点に設定し、
        // 必要ならば出発地点にもどります
        if (!agent.pathPending && agent.remainingDistance < 0.8f)
        {
            destPoint = (destPoint + 1) % points.Length;
            Debug.Log("point遷移");
        }

        Debug.Log("ExcutePatrol");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
}
