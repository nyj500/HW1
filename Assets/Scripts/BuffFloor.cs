using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFloor : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement pm;
    public PlayerShooting ps;
    
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("buff" + other.gameObject.name);
        float playerSpeed = pm.currentSpeed;
        float playerMaxSpeed = pm.maxSpeed;
        float playerAcceleration = pm.acceleration;
        float bulletSpeed = ps.bulletSpeed;
        
        playerMaxSpeed = 80.0f;
        playerSpeed = 80.0f;
        playerAcceleration = 40.0f;
        bulletSpeed = 300.0f;
    }

}
