using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMoney : MonoBehaviour
{
    private bool _actice = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !_actice)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100000);
            PlayerPrefs.SetInt("numOfLevel", 7);
            PlayerPrefs.SetInt("numOfChapter", 2);
            PlayerPrefs.SetInt("Leadership", 2505);
            _actice = true;
        }
    }
}
