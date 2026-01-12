using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    [SerializeField]
    private TextMeshProUGUI bestScoreText;

    [SerializeField]
    private GameObject gameEndView;

    [SerializeField]
    private GameObject adView;

    [SerializeField]
    private Button adButton;

    [SerializeField]
    private Button endButton;
    #endregion

    private void Awake()
    {
        ScoreManager.ChangeScoreEvent += ChangeScore;
        GameManager.GameOverEvent += ShowReviveAdView;
        GameManager.GameEndEvent += ShowGameEndView;
        adButton.onClick.AddListener(ShowReviveAd);
        endButton.onClick.AddListener(GameEnd);
    }

    private void OnDestroy()
    {
        ScoreManager.ChangeScoreEvent -= ChangeScore;
        GameManager.GameOverEvent -= ShowReviveAdView;
        GameManager.GameEndEvent -= ShowGameEndView;
    }

    private void ChangeScore(int score)
    {
        currentScoreText.text = score.ToString();
    }

    private void ShowReviveAd()
    {
        AdManager.Instance.ShowRewardAd();
        adView.SetActive(false);
    }

    private void ShowReviveAdView()
    {
        adView.SetActive(true);
    }

    private void GameEnd()
    {
        adView.SetActive(false);
        GameManager.GameEndEvent?.Invoke();
    }

    private void ShowGameEndView()
    {
        gameEndView.SetActive(true);
        bestScoreText.text = $"<sprite=0> {PlayerPrefs.GetInt("BestScore")}";
    }
}
