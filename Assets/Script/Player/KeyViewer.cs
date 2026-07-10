using UnityEngine;
using UnityEngine.UI;

public class KeyViewer : MonoBehaviour
{

    public Image W_Image;
    public Image A_Image;
    public Image S_Image;
    public Image D_Image;
    public Image Shift_Image;
    public Image W_Image2;
    //public Image Space_Image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            W_Image.color = Color.yellow;
            W_Image2.color = Color.yellow;
        }
        else
        {
            W_Image.color = Color.gray;
            W_Image2.color = Color.gray;
        }

        if (Input.GetKey(KeyCode.A))
                A_Image.color = Color.yellow;
            else
                A_Image.color = Color.gray;

        if (Input.GetKey(KeyCode.S))
            S_Image.color = Color.yellow;
        else
            S_Image.color = Color.gray;

        if (Input.GetKey(KeyCode.D))
            D_Image.color = Color.yellow;
        else
            D_Image.color = Color.gray;

        if (Input.GetKey(KeyCode.LeftShift))
            Shift_Image.color = Color.yellow;
        else
            Shift_Image.color = Color.gray;

/*
        if (Input.GetKey(KeyCode.Space))
            Space_Image.color = Color.yellow;
        else
            Space_Image.color = Color.gray;
*/

    }
}
