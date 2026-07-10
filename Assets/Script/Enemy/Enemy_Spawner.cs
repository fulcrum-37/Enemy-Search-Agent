using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    [SerializeField]
    GameObject enemyPrefab;
    public Transform[] points;

    public float startTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = 15f;
        Invoke(nameof(SpawnEnemy), startTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        Patrol2 patrol = enemy.GetComponent<Patrol2>();
        //patrol.points = points;
        //Debug.Log(patrol.points.Length);
    }
}
