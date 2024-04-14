using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMoney : MonoBehaviour
{
    private bool _actice = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && Input.GetKeyDown(KeyCode.E) && Input.GetKeyDown(KeyCode.G) &&
            Input.GetKeyDown(KeyCode.R) && !_actice)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 100000);
            _actice = true;
        }
    }
}
