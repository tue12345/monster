using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using static System.Net.WebRequestMethods;

public class GameManager : MonoBehaviour
{

    public GameObject[] ViewPorts; //head view ,eye view .....
    public Image[] Icons;
    //
    [SerializeField] GameObject[] Tick, Text, headSelected, eyeSelected, mouthSelected, accSelected, bodySelected, AdsReward, BG;
    [SerializeField] Animator _Sound, _Music;
    public Button PreviousButton, NextButton, restartBtn;
    //tuong tac sau khi nhan qua daily
    [SerializeField] Button[] Daily;
    //ani cho body
    [SerializeField] SkeletonAnimation bodyAni;
    [SerializeField] GameObject bodyskeleton;
    [SerializeField] Animator bodyAnimator;
    bool eyeselec = false, mouthselec = false, accselec = false;

    Vector3 NomalScale = new Vector3(1, 1, 1);
    [SerializeField] TMP_Text Scale, Money;
    int scaleEye = 5, scaleMouth = 5, scaleAcc = 5;

    string Url = "https://play.google.com/store/apps/details?id=com.mix.monster.makeover.blue.banban.diy.emoji&pli=1";

    int colecCount = 1;

    [SerializeField] GameObject[] lockdaily;

    int _Head, _Eye, _Mouth, _Acc, _Body;
    Vector2 _EyePosChange, _MouthPosChange, _AccPosChange;

    public GameObject[] heads, eyes, mouths, accs, bodys;
    public AudioClip PreviousBtnClip, NextBtnClip, click;
    public AudioClip[] BGMusic;
    public AudioSource source1, source2, source3;
    public GameObject GamePanel, WinPanel, SettingPanel, NointernetPanel, RatingPanel, DragToMove, Scalepanel, RemoveAdsPanel, NoAdsButton, HomeSettingPanel, ZoomUI;//game UI
    public GameObject[] ads;//icon ads tren cac button
    public VideoClip[] videos;
    public VideoPlayer videoPlayer;
    [SerializeField] AudioClip[] WinBGMusic;
    [SerializeField] AudioSource source4;//wingame
    [SerializeField] GameObject videoWin;
    //Action isConnected;
    string[] bodySkinName = { "1", "2", "3", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
    string[] bodyDance = { "dancing1", "dancing2", "dancing3" };
    int currentDance = 0;
    [SerializeField] GameObject loaddingPanel;
    [SerializeField] Image loadding;
    [SerializeField] GameObject TutorialPanel;
    [HideInInspector]
    public int selectedHead, selectedEye, selectedMouth, selectedAcc, selectedBody;
    enum Item
    {
        Head = 0, Eye = 1, Mouth = 2, Acc = 3, Body = 4
    }
    enum body
    {
        Blue = 0
    }
    Item currentItem;
    [HideInInspector]
    public GameObject selectedObject;
    public static GameManager instance;

    public int Body { get => _Body; set => _Body = value; }

    void Awake()
    {
        
        if(PlayerPrefs.GetInt("firt",1) == 1)
        {
            TutorialPanel.SetActive(true);
            PlayerPrefs.SetInt("firt", 0);
        }
        else
        {
            TutorialPanel.SetActive(false);
        }
        foreach(int daily in PlayerData.current.daily)
        {
            lockdaily[daily].gameObject.SetActive(false);
            Daily[daily].GetComponent<Button>().interactable = true;
        }
        videoWin.SetActive(false);
        bodyskeleton.SetActive(false);
        //bodyAni.skeleton.SetSkin("default");
        Scalepanel.SetActive(false);
        DragToMove.SetActive(false);
        eyeselec = false;
        mouthselec = false;
        accselec = false;
        colecCount = 0;
        reset();

        scaleEye = 5; scaleMouth = 5; scaleAcc = 5;

        SettingPanel.SetActive(false);
        source1.Play();
        //StartCoroutine(checkInternetConnection (isConnected));
        
        
        
    }
    void Update()
    {
        
        EnableAndDisableBtns();
        foreach (int i in PlayerData.current.adsReward)
        {
            AdsReward[i].SetActive(false);
        }
        if (currentItem == Item.Body && selectedBody == -1)
        {
            NextButton.interactable = false;
            //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
        }
        else if (currentItem == Item.Body && selectedBody != -1)
        {
            NextButton.interactable = true;
            //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Done";
        }


        if (PlayerPrefs.GetInt("CanPlayMusic") == 0)// 0 = music/sound off , 1 = music/sound on
        {
            source1.volume = 0;
            source4.volume = 0;
            _Music.SetBool("IsOn", false);

        }
        else if (PlayerPrefs.GetInt("CanPlayMusic") == 1)
        {
            source1.volume = 0.7f;
            source4.volume = 0.7f;
            _Music.SetBool("IsOn", true);
        }


        if (PlayerPrefs.GetInt("CanPlaySounds") == 0)
        {
            source2.volume = 0;
            source3.volume = 0;
            _Sound.SetBool("IsOn", false);
        }
        else if (PlayerPrefs.GetInt("CanPlaySounds") == 1)
        {
            source2.volume = 1f;
            source3.volume = 1f;
            _Sound.SetBool("IsOn", true);
        }

        /*if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            NointernetPanel.SetActive(false);
        }*/


        if (currentItem == Item.Body || currentItem == Item.Head)
        {
            Scalepanel.SetActive(false);
            DragToMove.SetActive(false);

        }
        if (currentItem == Item.Eye && eyeselec == true)
        {

            Scalepanel.SetActive(true);
            DragToMove.SetActive(true);

        }
        if (currentItem == Item.Mouth && mouthselec == true)
        {

            Scalepanel.SetActive(true);
            DragToMove.SetActive(true);


        }
        if (currentItem == Item.Acc && accselec == true)
        {

            Scalepanel.SetActive(true);
            DragToMove.SetActive(true);


        }
    }
    void reset()//reset trang thai
    {
        // GamePanel.SetActive(true);
        // WinPanel.SetActive(false);
        /*PlayerPrefs.SetInt("firt", 1);
        TutorialPanel.SetActive(true);*/
        selectedHead = selectedEye = selectedMouth = selectedAcc = selectedBody = -1;
        currentItem = Item.Head;
        instance = this;
        // Camera.main.orthographicSize = 10f;
        // Camera.main.transform.position = new Vector3(0, 0, -10);
        source1.clip = BGMusic[UnityEngine.Random.Range(0, BGMusic.Length)];
        // videoPlayer.gameObject.SetActive(false);
        for (int i = 0; i < ViewPorts.Length; i++)
        {
            if (i == (int)currentItem)
            {
                ViewPorts[i].SetActive(true);
            }
            else
            {
                ViewPorts[i].SetActive(false);
            }

        }




    }
    public void GoToMenu()
    {
        
        
        StartCoroutine(home(7));
    }
    public void RestartBtn()
    {

        /*PlayerData.current.money.Add(Winpanel.instance.MoneyText);
        Model.Instance.Save();*/
        PlayerData.current.Dance.Add(currentDance);
        //bộ phận
        PlayerData.current.Head.Add(_Head);
        PlayerData.current.Eye.Add(_Eye);
        PlayerData.current.Mouth.Add(_Mouth);
        PlayerData.current.Acc.Add(_Acc);
        PlayerData.current.Body.Add(Body);
        //scale
        PlayerData.current.ScaleEye.Add(scaleEye);
        PlayerData.current.ScaleMouth.Add(scaleMouth);
        PlayerData.current.ScaleAcc.Add(scaleAcc);
        //tọa độ
        PlayerData.current.PosEye.Add(_EyePosChange);
        PlayerData.current.PosMouth.Add(_MouthPosChange);
        PlayerData.current.PosAcc.Add(_AccPosChange);
        Model.Instance.Save();
        //tracking
        
        
        colecCount = 0;
        foreach (int i in PlayerData.current.Head)
        {
            colecCount++;
        }
        int colec = colecCount;
        if (colec % 2 == 0)
        {
            StartCoroutine(waitratepanel());
        }
        else
        {

            SceneManager.LoadScene(2);
        }
        
        ///////////////////////show ads /////////////////////////////////////////


    }


    public void Next()
    {
        
        Scalepanel.SetActive(false);
        DragToMove.SetActive(false);
        


        if (currentItem != Item.Body)
        {



            source2.clip = NextBtnClip;
            source2.Play();

            if (currentItem == Item.Acc)
            {
                Camera.main.orthographicSize = 19.5f;
                Camera.main.transform.position = new Vector3(0, -8, -10);
            }
            currentItem = currentItem + 1;
            for (int i = 0; i < ViewPorts.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    ViewPorts[i].SetActive(true);
                }
                else
                {
                    ViewPorts[i].SetActive(false);
                }

            }
            for (int i = 0; i < Icons.Length; i++)
            {

                if (i == (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", true);
                    Text[i].SetActive(true);
                }
                else if (i < (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                    Tick[i].SetActive(true);
                    Text[i].SetActive(false);
                }
                else
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                    Tick[i].SetActive(false);
                    Text[i].SetActive(false);
                }
            }
        }
        else
        {

            if (selectedBody != -1)
            {
                // ///////////////// win                    //////                         /////////////////////////
                
                videoWin.SetActive(true);
                videoPlayer.clip = videos[UnityEngine.Random.Range(0, 4)];
                source1.Stop();
                source4.clip = WinBGMusic[UnityEngine.Random.Range(0, 8)];
                source4.Play();
                source4.volume = 0.5f;
                videoPlayer.Play();
                StartCoroutine(WaitVideo());
                Camera.main.orthographicSize = 16;
                Camera.main.transform.position = new Vector3(0, -3, -10);


                _EyePosChange = eyes[selectedEye].transform.localPosition;
                _MouthPosChange = mouths[selectedMouth].transform.localPosition;
                _AccPosChange = accs[selectedAcc].transform.localPosition;

                //Debug.Log(_Head);
                
                //lưu dữ liệu
                //Model.Instance.Save();


            }

        }
        if (currentItem == Item.Eye)
        {
            Scale.text = scaleEye.ToString();
        }
        if (currentItem == Item.Mouth)
        {
            Scale.text = scaleMouth.ToString();
        }
        if (currentItem == Item.Acc)
        {
            Scale.text = scaleAcc.ToString();
        }



    }

    public void Previous()
    {
        
        
        source2.clip = PreviousBtnClip;
        source2.Play();
        //NextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next";
        Camera.main.orthographicSize = 10;
        Camera.main.transform.position = new Vector3(0, 0, -10);
        if (currentItem != Item.Head)
        {
            currentItem = currentItem - 1;
            for (int i = 0; i < ViewPorts.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    ViewPorts[i].SetActive(true);
                }
                else
                {
                    ViewPorts[i].SetActive(false);
                }

            }
            for (int i = 0; i < Icons.Length; i++)
            {
                if (i == (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", true);
                    Tick[i].SetActive(false);
                    Text[i].SetActive(true);
                }
                else if (i < (int)currentItem)
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                    Tick[i].SetActive(true);
                    Text[i].SetActive(false);
                }
                else
                {
                    Icons[i].GetComponent<Animator>().SetBool("Selected", false);
                    Tick[i].SetActive(false);
                    Text[i].SetActive(false);
                }
            }
        }
        if (currentItem == Item.Eye)
        {
            Scale.text = scaleEye.ToString();
        }
        if (currentItem == Item.Mouth)
        {
            Scale.text = scaleMouth.ToString();
        }
        if (currentItem == Item.Acc)
        {
            Scale.text = scaleAcc.ToString();
        }
    }

    void EnableAndDisableBtns()
    {

        if (currentItem == Item.Head)
        {
            PreviousButton.interactable = false;

        }
        else
        {
            PreviousButton.interactable = true;

        }
        if (currentItem == Item.Body && selectedBody == -1)
        {
            NextButton.interactable = false;
        }
        else
        {

            switch (currentItem)
            {
                case Item.Head:
                    if (selectedHead == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Eye:
                    if (selectedEye == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Mouth:
                    if (selectedMouth == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                case Item.Acc:
                    if (selectedAcc == -1) { NextButton.interactable = false; } else { NextButton.interactable = true; }
                    break;
                default:

                    break;
            }


            //  NextButton.interactable = true;

        }
    }


    public void itemButtom(int n)
    {
        
        source3.Play();
        switch (currentItem)
        {
            case Item.Head:
                selectedHead = n;
                Scalepanel.SetActive(false);
                DragToMove.SetActive(false);
                for (int i = 0; i < heads.Length; i++)
                {


                    if (i == n)
                    {
                        heads[i].SetActive(true);
                        headSelected[i].SetActive(true);
                        
                        _Head = n;
                    }
                    else
                    {
                        heads[i].SetActive(false);
                        headSelected[i].SetActive(false);
                    }
                }

                break;
            case Item.Eye:
                selectedEye = n;

                eyeselec = true;
                for (int i = 0; i < eyes.Length; i++)
                {

                    if (i == n)
                    {
                        eyes[i].GetComponent<Animator>().enabled = true;
                        eyes[i].SetActive(true); selectedObject = eyes[i];
                        eyeSelected[i].SetActive(true);
                        StartCoroutine(WaitAnimationEye());
                        _Eye = n;
                        //eyes[i].transform.localScale = NomalScale * (scaleEye / 5f);
                    }
                    else
                    {
                        eyes[i].SetActive(false);
                        eyeSelected[i].SetActive(false);
                    }
                }
                break;
            case Item.Mouth:
                selectedMouth = n;

                mouthselec = true;
                for (int i = 0; i < mouths.Length; i++)
                {

                    if (i == n)
                    {
                        mouths[i].GetComponent<Animator>().enabled = true;
                        mouths[i].SetActive(true); selectedObject = mouths[i];
                        mouthSelected[i].SetActive(true);
                        StartCoroutine(WaitAnimationMouth());
                        _Mouth = n;
                        //mouths[i].transform.localScale = NomalScale * (scaleMouth / 5f);
                    }
                    else
                    {
                        mouths[i].SetActive(false);
                        mouthSelected[i].SetActive(false);
                    }
                }
                break;
            case Item.Acc:
                selectedAcc = n;

                accselec = true;
                for (int i = 0; i < accs.Length; i++)
                {

                    if (i == n)
                    {
                        accs[i].GetComponent<Animator>().enabled = true;
                        accs[i].SetActive(true); selectedObject = accs[i];
                        accSelected[i].SetActive(true);
                        StartCoroutine(WaitAnimationAcc());
                        //accs[i].transform.localScale = NomalScale * (scaleAcc / 5f);
                        _Acc = n;
                    }
                    else
                    {
                        accs[i].SetActive(false);
                        accSelected[i].SetActive(false);
                    }
                }
                break;
            case Item.Body:
                selectedBody = n;
                Scalepanel.SetActive(false);
                DragToMove.SetActive(false);
                for (int i = 0; i < bodys.Length; i++)
                {
                    if (i == n)
                    {
                        bodyAnimator.SetTrigger("end");
                        bodyskeleton.SetActive(true);
                        bodyAnimator.SetTrigger("invoke");
                        bodyAni.skeleton.SetSkin(bodySkinName[selectedBody]);
                        //bodys[i].SetActive(true);
                        bodySelected[i].SetActive(true);

                        Body = n;

                    }
                    else
                    {
                        bodys[i].SetActive(false);
                        bodySelected[i].SetActive(false);
                    }
                }

                break;
            default:
                break;
        }

    }

    public void Sound()
    {
        if (PlayerPrefs.GetInt("CanPlaySounds") == 0)
        {
            PlayerPrefs.SetInt("CanPlaySounds", 1);

        }
        else
        {
            PlayerPrefs.SetInt("CanPlaySounds", 0);

        }

    }

    public void Music()
    {

        if (PlayerPrefs.GetInt("CanPlayMusic") == 0)
        {
            PlayerPrefs.SetInt("CanPlayMusic", 1);

        }
        else
        {
            PlayerPrefs.SetInt("CanPlayMusic", 0);

        }
    }

    public void SettingButton()
    {
        
        SettingPanel.SetActive(true);
    }

    public void CloseSettingButton()
    {
        SettingPanel.SetActive(false);
    }

    public void closeRatingPanel()
    {
        RatingPanel.SetActive(false);
    }

    public void Rating()
    {
        Application.OpenURL(Url);
    }
    public void OpenRatingPanel()
    {
        RatingPanel.SetActive(true);
    }

    public void ZoomOut()
    {
        
        switch (currentItem)
        {
            case Item.Eye:
                {
                    if (scaleEye > 1)
                    {
                        Debug.Log(selectedEye);
                        scaleEye--;
                        ObjectSelected().transform.localScale = NomalScale * (scaleEye / 5f);
                        Scale.text = scaleEye.ToString();
                        Debug.Log(scaleEye + "eye");
                    }
                }
                break;
            case Item.Mouth:
                {
                    if (scaleMouth > 1)
                    {
                        scaleMouth--;
                        ObjectSelected().transform.localScale = NomalScale * (scaleMouth / 5f);
                        Scale.text = scaleMouth.ToString();
                        Debug.Log(scaleMouth + "mouth");
                    }
                }
                break;
            case Item.Acc:
                {
                    if (scaleAcc > 1)
                    {
                        scaleAcc--;
                        ObjectSelected().transform.localScale = NomalScale * (scaleAcc / 5f);
                        Scale.text = scaleAcc.ToString();
                        Debug.Log(scaleAcc + "acc");
                    }
                }
                break;
            default:
                break;
        }

    }
    public void ZoomIn()
    {
        
        switch (currentItem)
        {
            case Item.Eye:
                {
                    if (scaleEye < 10)
                    {
                        scaleEye++;
                        ObjectSelected().transform.localScale = NomalScale * (scaleEye / 5f);
                        Scale.text = scaleEye.ToString();
                        Debug.Log(scaleEye + "eye");
                    }
                }
                break;
            case Item.Mouth:
                {
                    if (scaleMouth < 10)
                    {
                        scaleMouth++;
                        ObjectSelected().transform.localScale = NomalScale * (scaleMouth / 5f);
                        Scale.text = scaleMouth.ToString();
                        Debug.Log(scaleMouth + "mouth");
                    }
                }
                break;
            case Item.Acc:
                {
                    if (scaleAcc < 10)
                    {
                        scaleAcc++;
                        ObjectSelected().transform.localScale = NomalScale * (scaleAcc / 5f);
                        Scale.text = scaleAcc.ToString();
                        Debug.Log(scaleAcc + "acc");
                    }
                }
                break;
            default:
                break;
        }

    }

    

    public void WallPaper()
    {
        //TrackingClass.ButtonTracking("change_wallpaper", "end_game");
            for (int i = 0; i < BG.Length; i++)
            {
                BG[i].SetActive(false);
            }
            BG[UnityEngine.Random.Range(0, 7)].SetActive(true);
        
    }

    public void OpenRemoveAdsPanel()
    {
        RemoveAdsPanel.SetActive(true);
    }
    public void HideRemoeAdsPanel()
    {
        RemoveAdsPanel.SetActive(false);
    }

    public void HideRatingPanel()
    {
        RatingPanel.SetActive(false);
    }
  
    public void MoreDance()
    {
        switch (currentDance)
        {
            case 0:
                {
                    currentDance = 1;
                    bodyAni.AnimationName = bodyDance[currentDance];
                    break;
                }
            case 1:
                {
                    currentDance = 2;
                    bodyAni.AnimationName = bodyDance[currentDance];
                    break;
                }
            case 2:
                {
                    currentDance = 0;
                    bodyAni.AnimationName = bodyDance[currentDance];
                    break;
                }
            default:
                {
                    break;
                }
        }

        
    }
    public GameObject ObjectSelected()
    {
        if (currentItem == Item.Eye)
        {
            return eyes[selectedEye];
        }
        else if (currentItem == Item.Mouth)
        {
            return mouths[selectedMouth];
        }
        else if (currentItem == Item.Acc)
        {
            return accs[selectedAcc];
        }
        else

        { return null; }
    }
    public void Tutorial()
    {
        TutorialPanel.SetActive(false);
    }
    System.Collections.IEnumerator waitratepanel()
    {
        RatingPanel.SetActive(true);
        yield return new WaitUntil(() => RatingPanel.activeSelf == false);
        SceneManager.LoadScene(2);
        
    }
    IEnumerator WaitAnimationEye()
    {
        yield return new WaitForSeconds(0.2f);
        ObjectSelected().GetComponent<Animator>().enabled = false;
        ObjectSelected().transform.localScale = NomalScale * (scaleEye / 5f);
    }
    IEnumerator WaitAnimationMouth()
    {
        yield return new WaitForSeconds(0.2f);
        ObjectSelected().GetComponent<Animator>().enabled = false;
        ObjectSelected().transform.localScale = NomalScale * (scaleMouth / 5f);
    }
    IEnumerator WaitAnimationAcc()
    {
        yield return new WaitForSeconds(0.2f);
        ObjectSelected().GetComponent<Animator>().enabled = false;
        ObjectSelected().transform.localScale = NomalScale * (scaleAcc / 5f);
    }
    IEnumerator WaitVideo()
    {
        yield return new WaitForSeconds(4f);
        videoWin.SetActive(false);
        restartBtn.enabled = true;
        GamePanel.SetActive(false);
        WinPanel.SetActive(true);
        HomeSettingPanel.SetActive(false);
        ZoomUI.SetActive(false);
        BG[0].SetActive(true);
        bodyAni.AnimationName = "dancing1";
    }
    IEnumerator home(float time)
    {
        loaddingPanel.SetActive(true);
        loadding.fillAmount = 0;
        float deltatime = time / 500;

        do
        {

            loadding.fillAmount += deltatime;
            yield return new WaitForSeconds(deltatime);

        }
        while (loadding.fillAmount < 1);

        loadding.fillAmount = 1;
        SceneManager.LoadScene(1);
        //yield return new WaitForSeconds(0.3f);

    }
}
