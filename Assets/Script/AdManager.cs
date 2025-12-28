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
            LoadRewardAd();
        });
    }

    private string _bannerId = "ca-app-pub-3940256099942544/6300978111";

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

    private string _frontId = "ca-app-pub-3940256099942544/1033173712";

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

    private void ShowFrontAd()
    {
        if (_frontAd != null && _frontAd.CanShowAd())
        {
            _frontAd.Show();
        }
    }

    private string _rewardId = "ca-app-pub-3940256099942544/5224354917";

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
        };

        _rewardAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardAd();
        };
    }

    private void ShowRewardAd()
    {
        if (_rewardAd != null && _rewardAd.CanShowAd())
        {
            _rewardAd.Show((Reward reward) =>
            {
                Debug.Log("¾È³ç");
            });
        }
    }
}
