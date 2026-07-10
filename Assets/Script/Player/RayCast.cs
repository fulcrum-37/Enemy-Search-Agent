using System.Drawing;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class RayCast : MonoBehaviour

{
    public Material gridMaterial;

    public Vector3 lastWallPosition;
    public bool wallPlaced;
    
     [SerializeField]
     Create_Wall createWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f))
        {
            gridMaterial.SetVector("_RayHitPosition", hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 hitPos = hit.point;

                //Vector3 placePoint = hitPos + hit.normal * 0.5f;

                int x = Mathf.FloorToInt(hitPos.x);
                int z = Mathf.FloorToInt(hitPos.z);
                float y = 2.0f;

                Vector3 placePos = new Vector3(x + 0.5f, y, z + 0.5f);

                //Debug.Log(placePos);
                lastWallPosition = placePos;
                wallPlaced = true;

                createWall.CreateWall(placePos);
            }
        }
    }
}
