using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _peasant;
    [SerializeField] private GameObject _knight;
    [SerializeField] private GameObject _mage;
    [SerializeField] private GameObject _archer;
    [SerializeField] private GameObject _horseman;

    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject settingsSliders;
    [SerializeField] private Button backButton;

    private FaderExample _faderExample;

    private bool _settingsShown;

    private void Start()
    {
        _faderExample = FindObjectOfType<FaderExample>();
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene(4);
    }
    
    public void NewGame()
    {
        PlayerPrefs.SetInt("TownHallLevel", 1);
        PlayerPrefs.SetInt("Coins", 1000);
        PlayerPrefs.SetInt("Leadership", 550);
        PlayerPrefs.SetInt("MineLevel", 1);
        PlayerPrefs.SetInt("firstSession", 0);
        PlayerPrefs.SetString("mainSave", String.Empty);
        _peasant.GetComponent<Class>().Count = 0;
        _archer.GetComponent<Class>().Count = 0;
        _mage.GetComponent<Class>().Count = 0;
        _knight.GetComponent<Class>().Count = 0;

        SceneManager.LoadScene("ForScenario");
    }
    
    public void ContinueGame()
    {
        SceneManager.LoadScene("LoadScene");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void SettingsMenu()
    {
        // SceneManager.LoadScene("Settings");

        mainMenuButtons.SetActive(false);
        settingsSliders.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void QuitSettingsMenu()
    {
        mainMenuButtons.SetActive(true);
        settingsSliders.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void LoadChapter1()
    {
        SceneManager.LoadScene("FirstChapter");
    }
}
