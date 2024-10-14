using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField]
    private float hp = 50;
    [SerializeField]
    private float mp = 10;

    public float Hp { get => hp; set => hp = value; }
    public float Mp { get => mp; set => mp = value; }
}
