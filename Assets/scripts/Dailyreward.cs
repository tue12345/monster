using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dailyreward : MonoBehaviour
{
    // Start is called before the first frame update
    public int daycount;
    const int Day = 0;
    public DateTime nextRewardTime;
    [SerializeField] Button ClaimButton;
    [SerializeField] GameObject[] claimed,CanClaim;
    bool Claimed;
    void Start()
    {
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            PlayerPrefs.SetInt("Daily", -1);
            
            //Set first time opening to false
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);

            //Do your stuff here

        }
        else
        {
            Debug.Log("NOT First Time Opening");

            //Do your stuff here
        }
        //PlayerPrefs.SetInt("Daily", -1);
        //Claimed = false;
        daycount = PlayerPrefs.GetInt("Daily");
        //daycount = 0;
        for (int i = 0;i<=daycount;i++)
        {
            claimed[i].SetActive(true);
        }
        
        
        // Load the next reward time from PlayerPrefs
        nextRewardTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("NextRewardTime", DateTime.Now.Day.ToBinaryString())));
        //nextRewardTime = DateTime.Now;
    }

    void Update()
    {
        // Check if it's time to give the daily reward
        if (DateTime.Now.Day >= nextRewardTime.Day)
        {
            // Give the reward
            ClaimButton.interactable = true;
            
            
            Claimed = false;
            PlayerPrefs.SetInt("claim", 0);
            //PlayerPrefs.SetInt("Claimed", 0);
        }
        else if(DateTime.Now.Day < nextRewardTime.Day)
        {
            PlayerPrefs.SetInt("claim", 1);
            ClaimButton.interactable = false;
        }
        if (PlayerPrefs.GetInt("claim") == 0)
        {
            CanClaim[daycount+1].SetActive(true);
        }
        if(PlayerPrefs.GetInt("claim") == 1)
        {
            CanClaim[daycount].SetActive(false);
        }
    }
    public void Claim()
    {
        if(Claimed == false && daycount <7)
        {
            Claimed = true;
            PlayerPrefs.SetInt("claim", 1);
            //PlayerPrefs.SetInt("Daily", daycount);
            daycount++;
            claimed[daycount].SetActive(true);
            PlayerData.current.daily.Add(daycount);
            PlayerPrefs.SetInt("Daily", daycount);
            nextRewardTime = DateTime.Now.AddDays(1);
            PlayerPrefs.SetString("NextRewardTime", nextRewardTime.ToBinary().ToString());
            ClaimButton.interactable = false;
            //PlayerPrefs.SetInt("Claimed", 1);
        }
        Model.Instance.Save();
    }
}
