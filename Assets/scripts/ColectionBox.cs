using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ColectionBox : MonoBehaviour
{
    [SerializeField] int BoxNumber = 0;
    public static ColectionBox box;
    Vector3 normalScale = Vector3.one;
    [SerializeField] GameObject[] Head, Eye, Mouth, Acc, Body;
    [SerializeField] SkeletonGraphic bodyAni;
    string[] bodySkinName = { "1", "2", "3", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
    string[] bodyDance = { "dancing1", "dancing2", "dancing3" };
    string[] boneName = { "HEADBONE1", "HEADBONE1", "HEADBONE3", "HEADBONE1", "HEADBONE1", "HEADBONE1", "HEADBONE", "HEADBONE3", "HEADBONE7", "HEADBONE1", "HEADBONE9", "HEADBONE8", "HEADBONE7", "HEADBONE6", "HEADBONE5", "HEADBONE3" };
    [SerializeField] RectTransform eye, mouth, acc, head;
    [SerializeField] RectTransform root;
    [SerializeField] SkeletonUtilityBone rootbone;
    Vector3 rootpos;
    [SerializeField] RectTransform[] _eyeRect, _mouthRect, _accRect;
    Vector2 _PosEyeChange, _PosMouthChange, _PosAccChange;
    [SerializeField] TMP_Text money;
    public int BoxNumber1 { get => BoxNumber; set => BoxNumber = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        bodyAni.AnimationState.SetAnimation(0, bodyDance[PlayerData.current.Dance[BoxNumber1]], true);
        bodyAni.Skeleton.SetSkin(bodySkinName[PlayerData.current.Body[BoxNumber1]]);
        rootbone.boneName = boneName[PlayerData.current.Body[BoxNumber1]];
        rootbone.Reset();
        rootpos = root.localPosition;
        //bodyAni.Update();
        eye.SetParent(head);
        mouth.SetParent(head);
        acc.SetParent(head);
        //money
        money.text = PlayerData.current.money[BoxNumber];
        //hien thi cac bo phan
        Head[PlayerData.current.Head[BoxNumber1]].SetActive(true);
        Eye[PlayerData.current.Eye[BoxNumber1]].SetActive(true);
        Mouth[PlayerData.current.Mouth[BoxNumber1]].SetActive(true);
        Acc[PlayerData.current.Acc[BoxNumber1]].SetActive(true);
        //Body[PlayerData.current.Body[BoxNumber1]].SetActive(true);
        //scale
        Eye[PlayerData.current.Eye[BoxNumber1]].transform.localScale = normalScale * (PlayerData.current.ScaleEye[BoxNumber1] / 5f);
        Mouth[PlayerData.current.Mouth[BoxNumber1]].transform.localScale = normalScale * (PlayerData.current.ScaleMouth[BoxNumber1] / 5f);
        Acc[PlayerData.current.Acc[BoxNumber1]].transform.localScale = normalScale * (PlayerData.current.ScaleAcc[BoxNumber1] / 5f);
        //toa do cac bo phan
        _PosEyeChange = (PlayerData.current.PosEye[BoxNumber1]);
        _PosMouthChange = (PlayerData.current.PosMouth[BoxNumber1]);
        _PosAccChange = (PlayerData.current.PosAcc[BoxNumber1]);
        //eye pos
        Vector2 viewportPoseye = Camera.main.WorldToViewportPoint(_PosEyeChange);
        Vector2 WorldObject_ScreenPositionEye = new Vector2(
            ((viewportPoseye.x * _eyeRect[PlayerData.current.Eye[BoxNumber1]].sizeDelta.x) - (_eyeRect[PlayerData.current.Eye[BoxNumber1]].sizeDelta.x * 0.5f)),
            ((viewportPoseye.y * _eyeRect[PlayerData.current.Eye[BoxNumber1]].sizeDelta.y) - (_eyeRect[PlayerData.current.Eye[BoxNumber1]].sizeDelta.y * 0.5f)));
        _eyeRect[PlayerData.current.Eye[BoxNumber1]].anchoredPosition = WorldObject_ScreenPositionEye;
        //mouth pos
        Vector2 viewportPosMouth = Camera.main.WorldToViewportPoint(_PosMouthChange);
        Vector2 WorldObject_ScreenPositionMouth = new Vector2(
            ((viewportPosMouth.x * _mouthRect[PlayerData.current.Mouth[BoxNumber1]].sizeDelta.x) - (_mouthRect[PlayerData.current.Mouth[BoxNumber1]].sizeDelta.x * 0.5f)),
            ((viewportPosMouth.y * _mouthRect[PlayerData.current.Mouth[BoxNumber1]].sizeDelta.y) - (_mouthRect[PlayerData.current.Mouth[BoxNumber1]].sizeDelta.y * 0.5f)));
        _mouthRect[PlayerData.current.Mouth[BoxNumber1]].anchoredPosition = WorldObject_ScreenPositionMouth;
        //acc pos
        Vector2 viewportPosAcc = Camera.main.WorldToViewportPoint(_PosAccChange);
        Vector2 WorldObject_ScreenPositionAcc = new Vector2(
            ((viewportPosAcc.x * _accRect[PlayerData.current.Acc[BoxNumber1]].sizeDelta.x) - (_accRect[PlayerData.current.Acc[BoxNumber1]].sizeDelta.x * 0.5f)),
            ((viewportPosAcc.y * _accRect[PlayerData.current.Acc[BoxNumber1]].sizeDelta.y) - (_accRect[PlayerData.current.Acc[BoxNumber1]].sizeDelta.y * 0.5f)));
        _accRect[PlayerData.current.Acc[BoxNumber1]].anchoredPosition = WorldObject_ScreenPositionAcc;



    }
    
    private void FixedUpdate()
    {
        //headbone 1
        if (bodySkinName[PlayerData.current.Body[BoxNumber1]] == "1" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "2" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "5" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "6" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "7" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "11")
        {

            head.rotation = Quaternion.Euler(0, 0, (root.eulerAngles.z - 90));
        }
        //headbone 3
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "3"|| bodySkinName[PlayerData.current.Body[BoxNumber1]] == "9"|| bodySkinName[PlayerData.current.Body[BoxNumber1]] == "17")
        {
            head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
        }
        //headbone 7
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "10" || bodySkinName[PlayerData.current.Body[BoxNumber1]] == "14")
        {
            head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
        }
        //headbone 5
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "16")
        {
            head.rotation = Quaternion.Euler(0, 0, (root.eulerAngles.z));
        }
        //headbone
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "8")
        {
            head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
            head.localPosition = new Vector3(-1.093677e-06f, 210f, 0);
        }
        //headbone 9
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "12")
        {
            head.localPosition = new Vector3(-1.093677e-06f, 210f, 0);
        }
        //headbone 8
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "13")
        {
            head.rotation = Quaternion.Euler(0, 0, root.eulerAngles.z - 90);
        }
        //headbone 6
        else if(bodySkinName[PlayerData.current.Body[BoxNumber1]] == "15")
        {
            head.localPosition = new Vector3(-1.093677e-06f, (root.localPosition.y - rootpos.y)-155f, 0);
        }

    }

}
