using UnityEngine;

public class RayCast_Enemy : MonoBehaviour
{

    Human player;
    public bool playerHit;
    public Vector3 playerLastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Model").GetComponent<Human>();

    }

    // Update is called once per frame
    void Update()
    {
        playerHit = false;
        float viewAngle = 90;
        Vector3 dirToPlayer = (player.cameraTransform.position - transform.position).normalized;
        IsPlayerInSight(viewAngle, dirToPlayer);
    }

    //視界内判定
    public void IsPlayerInSight(float viewAngle, Vector3 dirToPlayer)
    {
        float dotProduct = Vector3.Dot(transform.forward, dirToPlayer); //内積

        if (dotProduct >= Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad))
        {
            GetHitObject(dirToPlayer);
            return;
        }
        else
            return;
    }

    //ヒットオブジェクト判定
    public void GetHitObject(Vector3 dirToPlayer)
    {
        Ray ray = new Ray(transform.position, dirToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, dirToPlayer * 100f, Color.red);
                playerHit = true;
                playerLastPosition = player.transform.position;
                Debug.Log("Hit");
                return;
            }
            else
            {
                Debug.Log("Nothing Hit");
                return;
            }
        }
        return;
    }
}
