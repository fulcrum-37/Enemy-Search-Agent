
using System;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    private const int MapWidth = 100;
    private const int MapHeight = 100;
    private CellState[,] map;
    MapVisualizer mapVisual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapVisual = GameObject.Find("MapVisualizer").GetComponent<MapVisualizer>();
        map = new CellState[MapWidth, MapHeight];

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                map[x, y] = CellState.Unknown;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2Int WorldToGrid(Vector3 Point)
    {
        float map_x = MapWidth / 2 - 0.5f;
        float map_y = MapHeight / 2 - 0.5f;
        int x = (int)(Point.x + map_x);
        int z = (int)(Point.z + map_y);

        return new Vector2Int(x, z);
    }

    public void EmptyCell(Vector2Int position, Vector2Int hitPoint)
    {
        Debug.Log("EmptyCell");
        //Bresenham
        int xa = position.x;
        int xb = hitPoint.x;
        int ya = position.y;
        int yb = hitPoint.y;

        //横方向に何マス進むか
        int dx = Math.Abs(hitPoint.x - position.x);
        //縦方向に何マス進むか
        int dy = Math.Abs(hitPoint.y - position.y);

        //横移動の向き
        int sx = (xb >= xa) ? 1 : -1;
        //縦移動の向き
        int sy = (yb >= ya) ? 1 : -1;

        Vector2Int emptyPoint;

        if (dx >= dy)  //dxの方が大きいのでxを移動させながらyを判定
        {
            int e = 2 * dy - dx;
            while (true)
            {
                emptyPoint = new Vector2Int(xa, ya);
                CellUpdate(emptyPoint, CellState.Empty);

                if (xa == xb)
                    break;

                if (e > 0)  //yを移動するか判定
                    {
                        ya += sy;
                        e -= 2 * dx;
                    }
                e += 2 * dy;
                xa += sx;
            }
        }
        else           //dyの方が大きいのでyを移動させながらxを判定
        {
            int e = 2 * dx - dy;
            while (true)
            {
                emptyPoint = new Vector2Int(xa, ya);
                CellUpdate(emptyPoint, CellState.Empty);

                if (ya == yb)
                    break;

                if (e > 0)  //xを移動するか判定
                {
                    xa += sx;
                    e -= 2 * dy;
                }
                e += 2 * dx;
                ya += sy;
            }
        }

    }

    public void CellUpdate(Vector2Int hitPoint, CellState state)
    {
        if (hitPoint.x < 0 || hitPoint.x >= MapWidth || hitPoint.y < 0 || hitPoint.y >= MapHeight)
            return;

        map[hitPoint.x, hitPoint.y] = state;
        mapVisual.MapUpdate(hitPoint.x, hitPoint.y, state);
    }

}
