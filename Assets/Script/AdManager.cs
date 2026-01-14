using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

    private BannerView _bannerAd;
    private InterstitialAd _frontAd;
    private RewardedAd _rewardAd;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MobileAds.Initialize((InitializationStatus status) =>
        {
            LoadBanner();
            LoadFrontAd();
            LoadRewardAd();
        });
    }

#if UNITY_EDITOR
    private string _bannerId = "ca-app-pub-3940256099942544/6300978111";
#else
    private string _bannerId = "ca-app-pub-4419256594849951/9797640567";
#endif

    private void LoadBanner()
    {
        if (_bannerAd != null)
        {
            _bannerAd.Destroy();
            _bannerAd = null;
        }

        _bannerAd = new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);
        _bannerAd.LoadAd(new AdRequest());
    }

#if UNITY_EDITOR
    private string _frontId = "ca-app-pub-3940256099942544/1033173712";
#else
    private string _frontId = "ca-app-pub-4419256594849951/9181072202";
#endif

    private void LoadFrontAd()
    {
        if (_frontAd != null)
        {
            _frontAd.Destroy();
            _frontAd = null;
        }

        InterstitialAd.Load(_frontId, new AdRequest(), (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                return;
            }

            _frontAd = ad;
            FrontEvent();
        });
    }

    private void FrontEvent()
    {
        _frontAd.OnAdFullScreenContentClosed += () =>
        {
            LoadFrontAd();
        };

        _frontAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadFrontAd();
        };
    }

    public void ShowFrontAd()
    {
        if (_frontAd != null && _frontAd.CanShowAd())
        {
            _frontAd.Show();
        }
    }

#if UNITY_EDITOR
    private string _rewardId = "ca-app-pub-3940256099942544/5224354917";
#else
    private string _rewardId = "ca-app-pub-4419256594849951/1605942314";
#endif

    private void LoadRewardAd()
    {
        if (_rewardAd != null)
        {
            _rewardAd.Destroy();
            _rewardAd = null;
        }

        RewardedAd.Load(_rewardId, new AdRequest(), (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                return;
            }

            _rewardAd = ad;
            RewardEvent();
        });
    }

    private void RewardEvent()
    {
        _rewardAd.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardAd();
            GameManager.ReviveEvent?.Invoke();
        };

        _rewardAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardAd();
        };
    }

    public void ShowRewardAd()
    {
        if (_rewardAd != null && _rewardAd.CanShowAd())
        {
            _rewardAd.Show((Reward reward) =>
            {
            });
        }
    }
}
