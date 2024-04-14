using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheck1 : MonoBehaviour
{
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject dialogStarter;
    [SerializeField] private Image image;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("firstSession") == 1)
        {
            dialog.SetActive(true);
            image.gameObject.SetActive(true);
            dialogStarter.SetActive(true);
        }
    }

    public void EndTutorial()
    {
        PlayerPrefs.SetInt("firstSession", 2);
    }
}
