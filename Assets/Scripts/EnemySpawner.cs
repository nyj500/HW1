using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // 적 프리팹
    public Vector3 spawnAreaMin;  // 스폰 영역 최소 값
    public Vector3 spawnAreaMax;  // 스폰 영역 최대 값
    public int enemyCount = 5;  // 한 번에 스폰할 적 수
    public float spawnInterval = 5.0f;  // 몇 초마다 스폰할지 설정

    void Start()
    {
        // 처음 2초 후에 시작하여, 5초마다 SpawnEnemies 함수를 호출
        InvokeRepeating("SpawnEnemies", 2.0f, spawnInterval);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // 랜덤 위치 계산
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // 적 생성
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
}
