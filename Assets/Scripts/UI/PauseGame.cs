using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool _isPaused = true;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject button;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;
            if (_isPaused) Time.timeScale = 1;
            if (!_isPaused) Time.timeScale = 0;
            canvas.SetActive(!_isPaused);
            button.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        _isPaused = !_isPaused;
        if (_isPaused) Time.timeScale = 1;
        if (!_isPaused) Time.timeScale = 0;
        canvas.SetActive(false);
        button.SetActive(true);
        AudioManager.instance.PlaySFX("Click");
    }

    public void SaveGame()
    {
        SaveSystem.Save("mainSave", GetSaveSnapshot());
        button.SetActive(false);
        AudioManager.instance.PlaySFX("Click");
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
        _isPaused = !_isPaused;
        if (_isPaused) Time.timeScale = 1;
        if (!_isPaused) Time.timeScale = 0;
        canvas.SetActive(false);
        button.SetActive(true);
        AudioManager.instance.PlaySFX("Click");
        AudioManager.instance.PlayMusic("Main Menu Music 1");
        SceneManager.LoadScene("MainMenu");
    }
}
