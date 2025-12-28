using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    [SerializeField]
    private GameObject gameEndView;

    [SerializeField]
    private Button adButton;

    private void Awake()
    {
        ScoreManager.ChangeScoreEvent += ChangeScore;
        GameManager.GameOverEvent += ShowAdView;
        GameManager.GameOverEvent += ShowGameEndView;
    }

    private void OnDestroy()
    {
        ScoreManager.ChangeScoreEvent -= ChangeScore;
        GameManager.GameOverEvent -= ShowAdView;
        GameManager.GameOverEvent -= ShowGameEndView;
    }

    private void ChangeScore(int score)
    {
        currentScoreText.text = score.ToString();
    }

    private void ShowAdView()
    {

    }

    private void ShowGameEndView()
    {
        gameEndView.SetActive(true);
        bestScoreText.text = $"<sprite=0> {PlayerPrefs.GetInt("BestScore")}";
    }
}
