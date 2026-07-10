using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class MapVisualizer : MonoBehaviour
{
    private const int textureHeight = 100;
    private const int textureWidth = 100;
    Texture2D texture;
    private RawImage rawImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texture = new Texture2D(textureHeight, textureWidth);

        rawImage = GameObject.Find("MapImage").GetComponent<RawImage>();
        Debug.Log(rawImage);

        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                texture.SetPixel(x, y, Color.gray);
            }
        }

        texture.Apply();
        rawImage.texture = texture;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MapUpdate(int x, int z,CellState state)
    {

        if (state == CellState.Wall)
            texture.SetPixel(x, z, Color.black);
        else if (state == CellState.Obstacle)
            texture.SetPixel(x, z, Color.green);
        else if (state == CellState.Empty)
            texture.SetPixel(x, z, Color.white);
        else
            return;

        texture.Apply();
        rawImage.texture = texture;
    }
}
