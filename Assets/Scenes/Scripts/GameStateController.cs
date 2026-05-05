using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    public static Action OnGameWin;
    public static Action OnGameLose;

    public bool GameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameOver()
    {
        GameOver = true;
    }
}
