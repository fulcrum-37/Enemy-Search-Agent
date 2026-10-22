using UnityEngine;

public class VisionSensor : MonoBehaviour
{

    Vector3 leftdir;
    RaycastHit hit;
    CellState state;
    CreateMap map;
    //Texture2D texture;
    private float viewAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        viewAngle = 100;
        leftdir = Quaternion.AngleAxis(-viewAngle / 2, Vector3.up) * transform.forward;
        map = GetComponentInParent<CreateMap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScanEnvironment()
    {
        int angle = 0;
        leftdir = Quaternion.AngleAxis(-viewAngle / 2, Vector3.up) * transform.forward;
        for (int i = 0; angle < viewAngle; i++)
        {
            leftdir = Quaternion.AngleAxis(5f, Vector3.up) * leftdir;
            angle += 5;

            Ray ray = new Ray(transform.position, leftdir);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.DrawRay(transform.position, leftdir * 100f, Color.green);
                HitObject();
            }

        }

        Vector2Int pos = map.WorldToGrid(transform.position);
        map.ApplyTexture(pos);

        //検出評価を入れる

        return;
    }

    void HitObject()
    {
        Vector2Int position;
        Vector2Int hitPoint;

        if (hit.collider.CompareTag("Wall"))
        {
            //Debug.Log("WallHit");
            state = CellState.Wall;
            position = map.WorldToGrid(transform.position);
            hitPoint = map.WorldToGrid(hit.point);
            map.EmptyCell(position, hitPoint);
            map.CellUpdate(hitPoint, state);
        }

        else if (hit.collider.CompareTag("Obstacle"))
        {
            //Debug.Log("ObstacleHit");
            state = CellState.Obstacle;
            position = map.WorldToGrid(transform.position);
            hitPoint = map.WorldToGrid(hit.point);
            map.EmptyCell(position, hitPoint);
            map.CellUpdate(hitPoint, state);
        }

        else
            return;
    }

}


