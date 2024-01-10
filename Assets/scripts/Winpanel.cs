using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR; 

public class Winpanel : MonoBehaviour
{
    int n;//,m;
    [SerializeField] TMP_Text money;

    [SerializeField] Animator ani;
    private string moneyText;
    //[SerializeField] GameObject light1, light2;
    public static Winpanel instance;
    [SerializeField] Transform root;
    [SerializeField] Transform head, eye, mouth, acc;
    [SerializeField] SkeletonUtilityBone rootfolow;
    string[] boneName = { "HEADBONE1", "HEADBONE1", "HEADBONE3", "HEADBONE1", "HEADBONE1", "HEADBONE1", "HEADBONE", "HEADBONE3", "HEADBONE7", "HEADBONE1", "HEADBONE9", "HEADBONE8", "HEADBONE7", "HEADBONE6", "HEADBONE5", "HEADBONE3" };
    //[SerializeField] Bone[] bonefolow;
    [SerializeField]
    GameObject[] button;
    [SerializeField] ParticleSystem FireCracker1, FireCracker2;
    public string MoneyText { get => moneyText; set => moneyText = value; }

    int image;
    
    
    // Start is called before the first frame update
    void Start()
    {
        image = PlayerPrefs.GetInt("image");
        rootfolow.boneName = boneName[GameManager.instance.Body];
        rootfolow.Reset();
        //ani = GetComponent<Animation>();
        n = 0;
        //m = 0;
        StartCoroutine(Fire());
        eye.SetParent(head);
        mouth.SetParent(head);
        acc.SetParent(head);
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (n >= 149)
        {
            n = 149;
        }
        if (n == 148)
        {
            //Debug.Log("play");
            ani.SetBool("isdone", true);


        }
        if (n % 5 == 0)
        {

            MoneyText = "$" + UnityEngine.Random.Range(1, 999).ToString() + "." + UnityEngine.Random.Range(100, 999).ToString() + "." + UnityEngine.Random.Range(100, 999).ToString();
            money.text = MoneyText;
        }

        n++;

        //head.position = headpos+ root.position;
        switch (GameManager.instance.Body)
        {
            //headbone 1
            case 0:
            case 1:
            case 3:
            case 4:
            case 5:
            case 9:
                {
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
                    head.position = new Vector3(-1.093677e-06f, 1.85f, 0); 
                    break;
                }
            //headbone 3
            case 2:
            case 7:
            case 15:
                {
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
                    break;
                }
            //headbone 7
            case 8:
            case 12:
                {
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
                    break;
                }
            //headbone
            case 6:
                {
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
                    head.position = new Vector3(-1.093677e-06f, 1.85f, 0);
                    break;
                }
            //headbone 9
            case 10:
                {
                    head.position = new Vector3(-1.093677e-06f, 2.64f, 0);
                    break;
                }
            //headbone 8
            case 11:
                {
                    head.position = new Vector3(-1.093677e-06f, 2.32f, 0);
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
                    break;
                }
            //headbone 6
            case 13:
                {
                    head.position = new Vector3(-1.093677e-06f,root.position.y, 0);
                    break;
                }
            //headbone 5
            case 14:
                {
                    head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z);
                    break;
                }
            default: { break; }
        }

    }
    public void moneyString()
    {
        Debug.Log(moneyText);
        PlayerData.current.money.Add(MoneyText);
        //Model.Instance.Save();
    }
    public void ScreenShot()
    {
        
        //TrackingClass.LevelEndTracking("win", "screen_shot", "none");
        image++;
        PlayerPrefs.SetInt("image", image);
        for(int i = 0;i<6; i++)
        {
            button[i].SetActive(false);
        }
        
        StartCoroutine(Screenshot());
        StartCoroutine(waitScreenShot());
    }
    IEnumerator Screenshot()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(CaptureScreen());
    }
    IEnumerator CaptureScreen()
    {
        yield return new WaitForEndOfFrame();

        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss");

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, fileName + ".png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        NativeGallery.SaveImageToGallery(ss.EncodeToPNG(), "MixMonster", fileName + ".png");

        // To avoid memory leaks
        Destroy(ss);
        new NativeShare().AddFile(filePath).SetSubject("ScreenShot").SetText("Check this out!").Share();
    }
    IEnumerator waitScreenShot()
    {
        yield return new WaitForSeconds(0.2f);
        for(int i = 0;i<6 ; i++)
        {
            button[i].SetActive(true) ;
        }
        
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(2);
        FireCracker1.Play();
        FireCracker2.Play();
        StartCoroutine(Fire());
    }
}
