using UnityEngine;

public class Create_Wall : MonoBehaviour
{

    [SerializeField]
    private GameObject blockPrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateWall(Vector3 placePos)
    {
        Instantiate(blockPrefab, placePos, Quaternion.identity);
    }
}
