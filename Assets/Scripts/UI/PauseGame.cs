using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool _isPaused = false;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject button;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
            Time.timeScale = Convert.ToInt16(_isPaused);
            canvas.SetActive(_isPaused);
            button.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        canvas.SetActive(false);
        button.SetActive(true);
    }

    public void SaveGame()
    {
        SaveSystem.Save("mainSave", GetSaveSnapshot());
        button.SetActive(false);
    }
    
    private ArmyData GetSaveSnapshot()
    {
        var data = new ArmyData()
        {
            Army = FindObjectOfType<OurHand>().Army,
        };

        return data;
    }
    
    public void ExitMenu()
    {
        Time.timeScale = 1;
        _isPaused = false;
        canvas.SetActive(false);
        button.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
}
