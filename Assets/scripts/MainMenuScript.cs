using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Application;


public class MainMenuScript : MonoBehaviour
{
    public GameObject[] heads;
    public GameObject SettingsPanel, DailyPanel, ColectionPanel, Claimed, NoAdsPanel, NoAdsButton;
    [SerializeField] Animator _Sound, _Music;
    [SerializeField] AudioClip[] BGM;
    [SerializeField] AudioSource source1;
    [SerializeField] Image loadding;
    [SerializeField] GameObject loaddingPanel;
    [SerializeField] Animator Playbutton;
    //[SerializeField] Slider _loadding;
    //[SerializeField] GameObject loaddingpanel;
    int n, item;
    float t, timer = .5f;
    private void Awake()
    {
        //_loadding.value = 0;
        Model.Instance.Load();

        //DailyPanel.SetActive(true);
        /*if(PlayerPrefs.GetInt("playing") == 0)
        {
            loaddingpanel.SetActive(true);
            PlayerPrefs.SetInt("playing", 1);
        }*/
    }
    private void Start()
    {
       
        //PlayerPrefs.SetInt("NoAds", 0);
        StartCoroutine(PlayAni());
        PlayerPrefs.SetInt("CanPlayMusic", 1);
        PlayerPrefs.SetInt("CanPlaySounds", 1);
        source1.clip = BGM[Random.Range(0,1)];
        source1.Play();
        if (PlayerPrefs.GetInt("NoAds") == 1)
        {
            NoAdsButton.SetActive(false);
        }
        foreach(string i in PlayerData.current.money)
        {
            Debug.Log(i);
        }
    }
    private void Update()
    {

        Screen.orientation = ScreenOrientation.Portrait;
        if (PlayerPrefs.GetInt("claim") == 0)
        {
            Claimed.SetActive(true);
        }
        else
        {
            Claimed.SetActive(false);
        }

        if (t < timer)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
            randomitem();
        }

        if (PlayerPrefs.GetInt("CanPlaySounds") == 0)
        {
            _Sound.SetBool("IsOn", false);
        }
        else { _Sound.SetBool("IsOn", true); }

        if (PlayerPrefs.GetInt("CanPlayMusic") == 0)
        {
            _Music.SetBool("IsOn", false);
        }
        else
        {
            _Music.SetBool("IsOn", true);
        }

    }


    public void startBtn()
    {
        //SceneManager.LoadScene(2);
        
        StartCoroutine(PlayeGame(6));
    }
    void randomitem()
    {

        n = Random.Range(0, heads.Length);
        for (int i = 0; i < heads.Length; i++)
        {
            if (i == n)
            {
                heads[i].SetActive(true);

            }
            else
            {
                heads[i].SetActive(false);
            }
        }

    }
    public void SoundButton()
    {
        if (PlayerPrefs.GetInt("CanPlaySounds") == 0)//0 == music/sound off 1 == music/sound on
        {
            PlayerPrefs.SetInt("CanPlaySounds", 1);
            //source2.volume = 0.7f;
            _Sound.SetBool("IsOn", true);
        }
        else
        {
            PlayerPrefs.SetInt("CanPlaySounds", 0);
            //source2.volume = 0;
            _Sound.SetBool("IsOn", false);
        }
    }
    public void MusicButton()
    {
        if (PlayerPrefs.GetInt("CanPlayMusic") == 0)
        {
            PlayerPrefs.SetInt("CanPlayMusic", 1);
            source1.volume = 1;
            _Music.SetBool("IsOn", true);
        }
        else
        {
            PlayerPrefs.SetInt("CanPlayMusic", 0);
            source1.volume = 0;
            _Music.SetBool("IsOn", false);
        }
    }
    public void ShowSettings()
    {
        
        SettingsPanel.SetActive(true);
    }
    public void HideSettings()
    {
        
        SettingsPanel.SetActive(false);

    }
    public void ShowDailyReward()
    {
        
        DailyPanel.SetActive(true);
    }
    public void HideDailyReward()
    {
        
        DailyPanel.SetActive(false);
    }
    public void ShowColection()
    {
        
        ColectionPanel.SetActive(true);
    }
    public void HideColection()
    {
        
        ColectionPanel.SetActive(false);
    }

    public void OpenReMoveAds()
    {
        NoAdsPanel.SetActive(true);
    }
    public void HideRemoveAds()
    {
        NoAdsPanel.SetActive(false);
    }
    /*public void BuyRemoveAds()
    {
        PlayerPrefs.SetInt("NoAds", 1);// 1 = da mua; 0 = chua mua
        NoAdsPanel.SetActive(false);
        NoAdsButton.SetActive(false);
        AdvertisementManager.Instance.HideBanner();
    }*/

    /*public void Policy()
    {
        TrackingClass.ButtonTracking("policy_button", "home");
        Debug.Log("web");
        GpmWebView.ShowUrl(
        "https://sites.google.com/teras.vn/mixmonster-policy/HomePage",
        new GpmWebViewRequest.Configuration()
        {
            style = GpmWebViewStyle.POPUP,
            orientation = GpmOrientation.PORTRAIT,
            isClearCookie = true,
            isClearCache = true,
            isNavigationBarVisible = false,
            supportMultipleWindows = true,
            isBackButtonVisible = true,
#if UNITY_IOS
            contentMode = GpmWebViewContentMode.MOBILE,
            isMaskViewVisible = true,
#endif
        });
        //Application.OpenURL("https://sites.google.com/teras.vn/mixmonster-policy/trang-ch%E1%BB%A7");
    }*/
    IEnumerator PlayeGame(float time)
    {
        loaddingPanel.SetActive(true);
        loadding.fillAmount = 0;
        float deltatime = time/500;
        
        do
        {
            
                loadding.fillAmount += deltatime;
                yield return new WaitForSeconds(deltatime);
            
        }
        while (loadding.fillAmount <1);
        
        loadding.fillAmount = 1;
        SceneManager.LoadScene(2);
        //yield return new WaitForSeconds(0.3f);

    }
    IEnumerator PlayAni()
    {
        Playbutton.SetTrigger("run");
        yield return new WaitForSeconds(1);
        Playbutton.SetTrigger("notRun");
        StartCoroutine(PlayAni());
    }
    /*public void CloseWebview()
    {
        GpmWebView.Close();
    }*/

}
