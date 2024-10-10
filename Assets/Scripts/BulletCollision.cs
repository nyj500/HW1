using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public PlayerShooting PlayerShooting;

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 물체가 "Enemy" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적을 파괴
            Destroy(collision.gameObject);

            // 적이 사라진 카운트를 ShootingController로 전달
            if (PlayerShooting != null)
            {
                PlayerShooting.EnemyDestroyed();
            }
        }

        // 총알을 파괴
        Destroy(gameObject);
    }
}
