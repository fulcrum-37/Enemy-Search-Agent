using UnityEngine;

public class Human : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform cameraTransform; // カメラ
    private CharacterController controller;
    Animator animator;
    Vector3 velocity;
    float gravity = -9.81f;

    bool wasGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float currentSpeed = speed;
        float x = 0f;
        float z = 0f;
        animator.SetFloat("VerticalVelocity", velocity.y);

        //移動処理-------------------------------------------------------------------------
        // WASD入力（押している間ずっと）
        if (Input.GetKey(KeyCode.W)) z += 1f;
        if (Input.GetKey(KeyCode.S)) z -= 1f;
        if (Input.GetKey(KeyCode.D)) x += 1f;
        if (Input.GetKey(KeyCode.A)) x -= 1f;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // 方向ベクトルを正規化（斜め移動の速度を一定にする）
        Vector3 direction = forward * z + right * x;

        //アニメーション--------------------------------------------------------------------
        //地面との衝突判定
        //bool isGrounded = transform.position.y <= 0.06f;
        bool isGrounded = controller.isGrounded;
        animator.SetBool("isGrounded", isGrounded);

        //ウォーキングのアニメーション
        bool isWalk = x != 0 || z != 0;
        animator.SetBool("isWalk", isWalk);

        //ランニングのアニメーション
        bool isRun = Input.GetKey(KeyCode.LeftShift) && (x != 0 || z != 0);
        animator.SetBool("isRun", isRun);
        if (isRun)
        {
            currentSpeed *= 2.5f;
        }

        //ジャンプのアニメーション
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = 6f;
        
        }

        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Landing");
        }

        //重力----------------------------------------------------------------------------
        //velocity.yの値を調整
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //重力
        velocity.y += gravity * Time.deltaTime;

        wasGrounded = isGrounded; 

        Vector3 move = direction * currentSpeed;
        controller.Move((move + velocity) * Time.deltaTime);
    }
}
