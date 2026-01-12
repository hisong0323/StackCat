using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public static Action<int> ChangeScoreEvent;

    private int currentScore;

    private int adScore;

    public int CurrentScore => currentScore;

    private void Awake()
    {
        Instance = this;
        GameManager.GameEndEvent += ShowAd;
    }

    private void OnDestroy()
    {
        GameManager.GameEndEvent -= ShowAd;
    }

    private void Start()
    {
        adScore = PlayerPrefs.GetInt("AdScore", 0);
    }

    public void IncreasesScore(int score)
    {
        if (GameManager.Instance.IsGameOver)
            return;

        currentScore += score;

        ChangeScoreEvent?.Invoke(currentScore);
        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (currentScore > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
    }

    private void ShowAd()
    {
        adScore += currentScore;

        if (adScore >= 50)
        {
            PlayerPrefs.SetInt("AdScore", 0);
            AdManager.Instance.ShowFrontAd();
        }
        else
        {
            PlayerPrefs.SetInt("AdScore", adScore);
        }
    }
}
