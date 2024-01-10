using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Model : Singleton_G<Model>
{
    private PlayerData playerData;

    public bool isLoaded = false;
    public Model()
    {

    }
    public void Save()
    {
        if (playerData == null || !isLoaded) return;

        string filePath = PlayerData.GetFilePath();
        string playerDataJson = JsonUtility.ToJson(playerData);
        //Debug.Log(playerDataJson);
        File.WriteAllText(filePath, playerDataJson);
        Debug.Log("save");
    }

    public void Load()
    {
        string filePath = PlayerData.GetFilePath();

        FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fileStream);
        string playerDataJson = sr.ReadToEnd();
        sr.Close();
        fileStream.Close();

        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);

        if (playerData == null)
        {
            playerData = new PlayerData();
        }
       
        isLoaded = true;
        PlayerData.current = playerData;

    }
}

