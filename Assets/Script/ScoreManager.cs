using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public static Action<int> ChangeScoreEvent;

    private int _score;

    private void Awake()
    {
        Instance = this;
        GameManager.GameOverEvent += UpdateBestScore;
    }

    private void OnDestroy()
    {
        GameManager.GameOverEvent -= UpdateBestScore;
    }

    public void IncreasesScore(int score)
    {
        _score += score;

        ChangeScoreEvent?.Invoke(_score);
    }

    private void UpdateBestScore()
    {
        if (_score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", _score);
        }
    }
}
