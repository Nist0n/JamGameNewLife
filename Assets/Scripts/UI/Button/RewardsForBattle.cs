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
            coins.text = "3500";
            leadership.text = "50";
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 2)
        {
            coins.text = "3650";
            leadership.text = "60";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 3)
        {
            coins.text = "3855";
            leadership.text = "65";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 4)
        {
            coins.text = "5500";
            leadership.text = "90";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 5)
        {
            coins.text = "9000";
            leadership.text = "120";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 6)
        {
            coins.text = "5000";
            leadership.text = "80";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 7) 
        {
            coins.text = "5200";
            leadership.text = "100";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 8)
        {
            coins.text = "5600";
            leadership.text = "105";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 9)
        {
            coins.text = "6000";
            leadership.text = "110";
        }
                
        if (PlayerPrefs.GetInt("numOfLevel") == 10)
        {
            coins.text = "15000";
            leadership.text = "150";
        }
    }
}
