using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 15.0f;
    public float moveRadius = 100.0f;  // 적이 이동할 범위
    private Vector3 targetPosition;

    void Start()
    {
        SetNewRandomTarget();
    }

    void Update()
    {
        // 적이 랜덤 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 지점에 도달하면 새로운 목표 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewRandomTarget();
        }
    }

    void SetNewRandomTarget()
    {
        // 랜덤한 목표 위치 설정
        targetPosition = new Vector3(
            Random.Range(transform.position.x - moveRadius, transform.position.x + moveRadius),
            transform.position.y,  // 수평 이동만 하려면 y 값을 고정
            Random.Range(transform.position.z - moveRadius, transform.position.z + moveRadius)
        );
    }
}
