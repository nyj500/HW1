using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;  // 이동 속도
    
    public float jumpForce = 1.0f;  // 점프 힘

    [SerializeField]
    float rotationSpeed = 150f;

    public Rigidbody rb;
    private bool isGrounded = true;
    private Animator anim;
    public float gravityScale = 2.0f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;  // Rigidbody의 회전은 물리적으로 고정
    }

    void FixedUpdate()
    {
        // WASD로 이동 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool isHKeyPressed = Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        bool isVKeyPressed = Mathf.Abs(Input.GetAxis("Vertical")) > 0;

        if (isHKeyPressed || isVKeyPressed)
        {
            // 이동 방향을 캐릭터가 바라보는 방향을 기준으로 설정
            Vector3 movement = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized * speed * Time.deltaTime;

            // if (movement != Vector3.zero)
            // {
            //     // 캐릭터가 이동하는 방향으로 회전
            //     Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            //     transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            // }

            rb.MovePosition(rb.position + movement);  // Rigidbody로 이동 처리
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
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
    }
}
