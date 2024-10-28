using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject MainCharacter;
    public Player player;
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;
    
    void Update()
    {
        if (playerShooting.enemyCount >= 30)
        {
            LoadWinScene();
        }
        if (player.Hp == 0)
        {
            LoadLoseScene();
        }
        if (MainCharacter.transform.position.y < -50.0f)
        {
            LoadLoseScene();
        }
    }

    public static void LoadLoseScene()
    {
        SceneManager.LoadScene("Scenes/LoseScene");
    }

    public static void LoadWinScene()
    {
        SceneManager.LoadScene("Scenes/WinScene");
    }
}
