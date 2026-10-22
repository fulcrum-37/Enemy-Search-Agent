using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol2 : MonoBehaviour
{

    private NavMeshAgent agent;
    Animator animator;
    VisionSensor sensor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        sensor = GetComponentInChildren<VisionSensor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ExecutePatrol()
    {
        sensor.ScanEnvironment();
    }

}
