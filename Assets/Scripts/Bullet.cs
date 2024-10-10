using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage = 10f;  // 총알이 가하는 데미지
    public float bulletLifetime = 2f;  // 총알이 사라지는 시간 (예: 2초 후에 사라짐)

    private void Start()
    {
        // 일정 시간 후에 총알을 삭제
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체가 적인지 확인
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적 오브젝트에 데미지 주기 (예시: 적에게 "TakeDamage" 메서드를 호출)
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // enemy.TakeDamage(bulletDamage);  // 적에게 데미지 전달
            }
        }

        // 충돌 후 총알을 삭제
        // Destroy(gameObject);
    }
}
