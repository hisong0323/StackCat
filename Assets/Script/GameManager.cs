using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action GameStartEvent;
    public static Action GameOverEvent;
    public static Action GameEndEvent;
    public static Action ReviveEvent;

    public bool IsGameOver { get; private set; }
    public bool HasRevive { get; private set; } 

    private void Awake()
    {
        Instance = this;
        GameOverEvent += OnGameOver;
        GameEndEvent += OnGameOver;
        ReviveEvent += OnRevive;
    }

    private void OnDestroy()
    {
        GameOverEvent -= OnGameOver;
        GameEndEvent -= OnGameOver;
        ReviveEvent -= OnRevive;
    }

    private void OnGameOver()
    {
        IsGameOver = true;
    }

    private void OnRevive()
    {
        HasRevive = true;
        IsGameOver = false;
    }
}
