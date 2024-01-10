using System;
using System.Collections.Generic;
using UnityEngine;
//Serializable]
public class PlayerData
{
    #region StaticConfig

#if UNITY_EDITOR
    private static readonly string directory = Environment.CurrentDirectory + "/";
#else
    private static readonly string directory = Application.persistentDataPath;
#endif
    private static string playerDataFileName = "playerdata" + ".txt";

    public static string GetFilePath()
    {
        return directory + playerDataFileName;
    }

    #endregion

    public static PlayerData current;
    /*public List<TaskView> BuyTaskData = new List<TaskView>();
    public List<TaskView> BuyTaskData_Selected = new List<TaskView>(); // muc dic de reset skin dang chon*/
    public List<int> Head = new List<int>();
    public List<int> Eye = new List<int>();
    public List<int> Mouth = new List<int>();
    public List<int> Acc = new List<int>();
    public List<int> Body = new List<int>();
    public List<int> ScaleEye = new List<int>();
    public List<int>ScaleMouth = new List<int>();
    public List<int> ScaleAcc = new List<int>();
    public List<Vector2> PosEye = new List<Vector2>();
    public List<Vector2> PosMouth = new List<Vector2>();
    public List<Vector2> PosAcc = new List<Vector2>();
    public List<int> daily = new List<int>();
    public List<int> adsReward = new List<int>();
    public List<string> money = new List<string>();
    public List<int> Dance = new List<int>();
    //

}