using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 150f;  // 마우스 감도
    public Transform playerBody;  // 캐릭터의 Transform
    private float xRotation = 0f;

    void Start()
    {
        // 마우스 커서를 화면 가운데 고정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력을 받아서 회전 처리
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 상하 회전은 카메라에서만 처리 (x축 회전)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 카메라 상하 회전 각도 제한

        // 카메라의 상하 회전 처리
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 캐릭터(플레이어)의 좌우 회전 처리 (y축 회전)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
