using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;

    public float acceleration = 20.0f;  // 가속도
    public float deceleration = 20.0f;  // 감속도
    public float currentSpeed = 0.0f;  // 현재 속도
    public float maxSpeed = 60.0f;  // 최대 속도
    public float jumpForce = 70.0f;  // 점프 힘

    public float rotationSpeed = 150f;


    public Rigidbody rb;
    private bool isGrounded = true;
    
    private Animator anim;
    public float gravityScale = 1.0f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;  // Rigidbody의 회전은 물리적으로 고정
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        // WASD로 이동 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0;

        bool isHKeyPressed = Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        bool isVKeyPressed = Mathf.Abs(Input.GetAxis("Vertical")) > 0;

        if (isHKeyPressed || isVKeyPressed)
        {
            // 이동 방향을 캐릭터가 바라보는 방향을 기준으로 설정
            Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * currentSpeed * Time.deltaTime;

            if (isMoving)
            {
                // 가속도 적용
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);  // 최대 속도 제한
            }
            rb.MovePosition(rb.position + movement);  // Rigidbody로 이동 처리
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            currentSpeed = 0;
        }

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);  // 더 큰 중력 적용
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // 점프 힘 적용
        Debug.Log("Jump");
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 땅에 닿으면 다시 점프 가능
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))  
        {
            gm.LoadLoseScene();
        }
    }
}
