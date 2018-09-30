using UnityEngine;
using System.Collections;
using admob;
public class AdManager : MonoBehaviour {
	public static AdManager Instance{ set; get;}

	public string BannerID;
	public string InstitialID;
    public string BannerIDIOS;
    public string InstitialIDIOS;

    // Use this for initialization
    private void Start () {
		Instance = this;
		DontDestroyOnLoad (gameObject);
        #if UNITY_ANDROID
		Admob.Instance ().initAdmob (BannerID ,InstitialID);
		Admob.Instance ().loadInterstitial ();
        #elif UNITY_IOS
        Admob.Instance ().initAdmob (BannerIDIOS ,InstitialIDIOS);
		Admob.Instance ().loadInterstitial ();
        #endif

    }
	public void ShowBanner()
	{
		#if (UNITY_ANDROID || UNITY_IOS)
		Admob.Instance ().showBannerRelative (AdSize.Banner, AdPosition.BOTTOM_CENTER, 5);
		#endif
	}
	public void HideBanner()
	{
		#if UNITY_EDITOR
		Debug.Log("HideBanner");
        #elif (UNITY_ANDROID || UNITY_IOS)
		Admob.Instance().removeBanner();
		#endif
	}
	public void ShowInterstitial() {
		if (Admob.Instance().isInterstitialReady ()) {
			Admob.Instance ().showInterstitial ();
		} else
        {
			Admob.Instance ().loadInterstitial ();
		}
	}
}
