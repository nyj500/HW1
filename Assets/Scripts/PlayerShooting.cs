using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;  // 총알 프리팹
    public Transform bulletSpawnPoint;  // 총알이 생성될 위치 (총구)
    public float bulletSpeed = 500f;  // 총알 속도
    public float bulletLifetime = 2f;  // 총알이 존재하는 시간 (사라지기 전까지)
    public Camera playerCamera;

    public LayerMask ignoreLayerMask;

    public int enemyCount = 0;
    public Text enemyCountText;

    public PlayerMovement pm;
    public GameObject bulletEffectPrefab;

    void Update()
    {
        // 마우스 클릭 시 총 발사
        if (Input.GetMouseButtonDown(0))  // 0 = 왼쪽 마우스 버튼
        {
            Shoot();
            Debug.Log("Shoot");
            PlayBulletEffect();
        }

        UpdateEnemyCountUI();
    }

    void PlayBulletEffect()
    {
        GameObject effectInstance = Instantiate(bulletEffectPrefab, bulletSpawnPoint.position, Quaternion.identity);

        Destroy(effectInstance, 1f);
    }

    void Shoot()
    {
        // 화면 중앙에서 Ray를 쏘아 충돌하는 물체 탐지
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayerMask))
        {
            // 충돌한 지점을 목표 지점으로 설정
            targetPoint = hit.point;
        }
        else
        {
            // 충돌하지 않았을 경우 먼 지점으로 총알을 발사
            targetPoint = ray.GetPoint(1000);  // 1000 유닛 앞의 지점
        }
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Rigidbody를 통해 총알에 힘을 가해 목표 지점으로 발사
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (targetPoint - bulletSpawnPoint.position).normalized;
        rb.velocity = direction * bulletSpeed;

        BulletCollision bulletCollision = bullet.AddComponent<BulletCollision>();
        bulletCollision.PlayerShooting = this;  

         // 일정 시간 후 총알 삭제
        Destroy(bullet, bulletLifetime);

    }

    public void EnemyDestroyed()
    {
        enemyCount++;
        if (enemyCount >= 5)
        {
            pm.jumpForce = 150f;
        }

        Debug.Log("Enemy destroyed. Count: " + enemyCount);
    }

    void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "KILL: " + enemyCount;
        }
    }
}

