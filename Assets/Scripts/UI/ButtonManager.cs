using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _peasant;
    [SerializeField] private GameObject _knight;
    [SerializeField] private GameObject _mage;
    [SerializeField] private GameObject _archer;
    [SerializeField] private GameObject _horseman;
    [SerializeField] private GameObject _zombie;
    [SerializeField] private GameObject _skeleton;
    [SerializeField] private GameObject _necromancer;
    [SerializeField] private GameObject _blackKnight;

    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject settingsSliders;
    [SerializeField] private Button backButton;

    private FaderExample _faderExample;
    public Image _image;

    private bool _settingsShown;

    private void Start()
    {
        _faderExample = FindObjectOfType<FaderExample>();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("TownHallLevel", 1);
        PlayerPrefs.SetInt("Coins", 1000);
        PlayerPrefs.SetInt("Leadership", 550);
        PlayerPrefs.SetInt("MineLevel", 1);
        PlayerPrefs.SetInt("MineCoins", 0);
        PlayerPrefs.SetInt("firstSession", 1);
        PlayerPrefs.SetInt("numOfLevel", 1);
        PlayerPrefs.SetInt("numOfChapter", 1);
        PlayerPrefs.SetString("mainSave", string.Empty);

        foreach (string key in Barracks.ResearchUnitKeys)
        {
            PlayerPrefs.SetInt(key, 0);
        }
        
        _peasant.GetComponent<Class>().Count = 0;
        _archer.GetComponent<Class>().Count = 0;
        _mage.GetComponent<Class>().Count = 0;
        _knight.GetComponent<Class>().Count = 0;
        _horseman.GetComponent<Class>().Count = 0;
        _necromancer.GetComponent<Class>().Count = 0;
        _zombie.GetComponent<Class>().Count = 0;
        _skeleton.GetComponent<Class>().Count = 0;
        _blackKnight.GetComponent<Class>().Count = 0;
        
        _peasant.GetComponent<Character>().Count = 0;
        _archer.GetComponent<Character>().Count = 0;
        _mage.GetComponent<Character>().Count = 0;
        _knight.GetComponent<Character>().Count = 0;
        _horseman.GetComponent<Character>().Count = 0;
        _necromancer.GetComponent<Character>().Count = 0;
        _zombie.GetComponent<Character>().Count = 0;
        _skeleton.GetComponent<Character>().Count = 0;
        _blackKnight.GetComponent<Character>().Count = 0;
        
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("ForScenario", "Plot");
    }
    
    public void ContinueGame()
    {
        string song = "Town";
        Random random = new Random();
        int rand = random.Next(1, 4);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        // OurHand.instance.UpdateHand();
        _faderExample.LoadScene("LoadScene", song);
    }

    public void SettingsMenu()
    {
        AudioManager.instance.PlaySFX("Click");
        _image.enabled = false;
        mainMenuButtons.SetActive(false);
        settingsSliders.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void QuitSettingsMenu()
    {
        AudioManager.instance.PlaySFX("Click");
        _image.enabled = true;
        mainMenuButtons.SetActive(true);
        settingsSliders.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        AudioManager.instance.PlaySFX("Click");
        Application.Quit();
        Debug.Log("Quit");
    }

    public void LoadLevel1()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_1", song);
    }
    
    public void LoadLevel2()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_2", song);
    }
    
    public void LoadLevel3()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_3", song);
    }
    
    public void LoadLevel4()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_4", song);
    }
    
    public void LoadLevel5()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_5", song);
    }
    
    public void LoadLevel6()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_6", song);
    }
    
    public void LoadLevel7()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_7", song);
    }
    
    public void LoadLevel8()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_8", song);
    }
    
    public void LoadLevel9()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_9", song);
    }
    
    public void LoadLevel10()
    {
        string song = "Battle";
        Random random = new Random();
        int rand = random.Next(1, 2);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Level_10", song);
    }

    public void LoadChapter1()
    {
        AudioManager.instance.PlaySFX("Click");
        if (PlayerPrefs.GetInt("numOfChapter") == 1) _faderExample.LoadScene("FirstChapter", "Main Menu Music 1");
        if (PlayerPrefs.GetInt("numOfChapter") == 2) _faderExample.LoadScene("SecondChapter", "Main Menu Music 1");
    }
    
    public void ButtonBackBase()
    {
        string song = "Town";
        Random random = new Random();
        int rand = random.Next(1, 4);
        song += rand;
        AudioManager.instance.PlaySFX("Click");
        _faderExample.LoadScene("Base", song);
    }
}
