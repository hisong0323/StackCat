using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VersionController : MonoBehaviour
{
    #region Field
    [SerializeField]
    private GameObject updatePopup;

    [SerializeField]
    private Button updateButton;

    [SerializeField]
    private bool isTestBuild;
    #endregion

    private readonly string _versionCheckUrl = "https://redeyeshq.github.io/stackcatVersion.txt"; //최신 버전 저장되어있는 Url
    private readonly string _testVersionCheckUrl = "https://redeyeshq.github.io/blockpuzzleTestVersion.txt"; //최신 버전 저장되어있는 Url


    private void Start()
    {
        updateButton?.onClick.AddListener(OnClickUpdateButton);
        CheckVersion().Forget();
    }

    private async UniTask CheckVersion()
    {
        string checkUrl = isTestBuild ? _testVersionCheckUrl : _versionCheckUrl;
        UnityWebRequest www = UnityWebRequest.Get(checkUrl);

        await www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string latestVersion = www.downloadHandler.text.Trim();
            string currentVersion = Application.version;

#if UNITY_EDITOR
            Debug.Log($"lastestVersion{latestVersion}");
            Debug.Log($"currentVersion{currentVersion}");
#endif

            if (latestVersion != currentVersion)
            {
                updatePopup.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else
        {
#if UNITY_EDITOR
            Debug.LogWarning("버전 확인 실패: " + www.error);
#endif
        }
    }

    private void OnClickUpdateButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
    }
}