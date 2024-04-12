using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleTest");
    }
    public void NewGame()
    {
        SceneManager.LoadScene("LoadScene");
        Debug.Log("LoadScene");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("LoadScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
