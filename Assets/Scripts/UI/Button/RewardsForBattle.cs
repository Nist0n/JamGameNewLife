using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardsForBattles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI leadership;

    private void Start()
    {
        if (PlayerPrefs.GetInt("numOfLevel") == 1)
        {
            coins.text = "4500";
            leadership.text = "80";
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 2)
        {
            coins.text = "4800";
            leadership.text = "85";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 3)
        {
            coins.text = "5300";
            leadership.text = "90";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 4)
        {
            coins.text = "7500";
            leadership.text = "110";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 5)
        {
            coins.text = "12500";
            leadership.text = "150";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 6)
        {
            coins.text = "7500";
            leadership.text = "80";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 7) 
        {
            coins.text = "7000";
            leadership.text = "100";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 8)
        {
            coins.text = "7500";
            leadership.text = "105";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 9)
        {
            coins.text = "8000";
            leadership.text = "110";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 10)
        {
            coins.text = "15000";
            leadership.text = "150";
        }
    }
}
