using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public float baseAttackDistance;
    public float speed = 5f;        // 이동 속도
    public float explosionRange = 4f; // 폭발 모드로 전환할 거리

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
                // 베이스 방향으로 이동
                Vector3 direction = (baseTransform.position - transform.position).normalized;
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
        currentState = EnemyState.Die;
    }

    void Explode()
    {
        // 폭발, 메인베이스에 피해 입힘
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        // 메인 베이스에 피해 입히기
        DealDamageToBase();
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
                Debug.LogWarning("BaseHealth 컴포넌트를 찾을 수 없습니다.");
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
