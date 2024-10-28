using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBase : MonoBehaviour
{
    public int hp = 100;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("베이스에 피해를 입었습니다! 남은 체력: " + hp);

        if (hp <= 0)
        {
            Debug.Log("베이스가 파괴되었습니다!");
            GameManager.LoadLoseScene();
        }
    }
}
