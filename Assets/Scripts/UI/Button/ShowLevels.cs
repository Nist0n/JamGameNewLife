using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;

    private void Update()
    {
        if (PlayerPrefs.GetInt("numOfLevel") == 1)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level1") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 2)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level2") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 3)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level3") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 4)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level4") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 5)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level5") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 6)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level6") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 7)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level7") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 8)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level8") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 9)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level9") level.SetActive(true);
                else level.SetActive(false);
            }
        }
        
        if (PlayerPrefs.GetInt("numOfLevel") == 10)
        {
            foreach (var level in levels)
            {
                if (level.name == "Level10") level.SetActive(true);
                else level.SetActive(false);
            }
        }
    }
}
