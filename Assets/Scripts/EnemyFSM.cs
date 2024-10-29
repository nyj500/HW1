using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        GoToBase,
        ExplosionMode,
        Die,
    };

    public EnemyState currentState;
    public Transform baseTransform;

    void Start()
    {
        currentState = EnemyState.GoToBase;
        GameObject baseObject = GameObject.FindWithTag("MainBase");
        baseTransform = baseObject.transform;
    }

    void Update()
    {
        if (currentState == EnemyState.GoToBase) { GoToBase(); }
        else if (currentState == EnemyState.ExplosionMode) { ExplosionMode(); }
        else { Die(); }
        
    }

    //public Sight sightSensor;
    
    public float baseRadius = 5f;
    public float speed = 5f;        // 이동 속도
    public float explosionRange = 5f; // 폭발 모드로 전환할 거리

    void GoToBase()
    {
        // 태어나면 계속 베이스로 향함. 베이스 근처에 가면 폭발 모드 
        if (baseTransform != null)
        {
            // 베이스와의 거리 계산
            float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);

            // 베이스 근처에 도착했을 때
            if (distanceToBase <= explosionRange)
            {
                currentState = EnemyState.ExplosionMode; // 폭발 모드 실행
            }
            else
            {
                // 베이스의 중심에서 현재 위치까지의 방향 벡터를 계산
                Vector3 directionToBase = new Vector3(transform.position.x - baseTransform.position.x, 0, transform.position.z - baseTransform.position.z).normalized;

                // 베이스의 옆면 테두리 중 가장 가까운 xz 평면상의 지점 계산
                Vector3 targetPosition = baseTransform.position - directionToBase * baseRadius;
                targetPosition.y = baseTransform.position.y; // y좌표 고정

                // 이동 방향 계산
                Vector3 direction = (targetPosition - transform.position).normalized;

                // 이동 방향으로 오브젝트 회전
                transform.rotation = Quaternion.LookRotation(direction);

                // 오브젝트 이동
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }

    public GameObject explosionEffectPrefab;
    public int damageToBase = 10;

    void ExplosionMode()
    {
        // 3초 뒤 폭발
        Invoke("Explode", 3f);
        
    }

    void Explode()
    {
        // 폭발, 메인베이스에 피해 입힘
        if (explosionEffectPrefab != null)
        {
            Vector3 effectPosition = new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z); 
            GameObject effectInstance = Instantiate(explosionEffectPrefab, effectPosition, Quaternion.identity);
            VisualEffect effect = effectInstance.GetComponent<VisualEffect>();
            effect.Play();
            Destroy(effectInstance, 1f); // 1초 후에 파괴
        }
        // 메인 베이스에 피해 입히기
        DealDamageToBase();
        currentState = EnemyState.Die;
    }

    void DealDamageToBase()
    {
        if (baseTransform != null)
        {
            // BaseHealth 컴포넌트에 접근하여 피해를 입힘
            MainBase mainBase = baseTransform.GetComponent<MainBase>();
            if (mainBase != null)
            {
                mainBase.TakeDamage(damageToBase);
            }
            else
            {
                Debug.LogWarning("MainBase 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }
}
