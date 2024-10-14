using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaiMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 30f;
    private float startX;
    
    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // 시간에 따라 X좌표를 이동시킴
        float newX = transform.position.x + speed * Time.deltaTime;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // 오브젝트가 maxDistance만큼 이동하면 다시 처음 위치로 돌아감
        if (Mathf.Abs(newX - startX) >= maxDistance)
        {
            // 초기 위치로 리셋
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
        }
    }
}
