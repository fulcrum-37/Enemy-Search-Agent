using UnityEngine;

public class Mouse : MonoBehaviour
{

    public float sensitivity = 200f;
    public Transform playerBody; // プレイヤー本体

    float xRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // 上下回転（カメラ）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 50f); // 見上げすぎ防止

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 左右回転（プレイヤー本体）
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
