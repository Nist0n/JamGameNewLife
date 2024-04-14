using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckArmy : MonoBehaviour
{
    [SerializeField] private OurHand _ourHand;
    [SerializeField] private Button[] positionsOfImages;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private ShowKickbutton showKickbutton;
    [SerializeField] private GameObject toBattleButton;
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
        _ourHand = FindObjectOfType<OurHand>();
    }

    public void OpenMenu()
    {
        toBattleButton.gameObject.SetActive(false);
        
        _canvas.gameObject.SetActive(true);
        
        for (int i = 0; i < _ourHand.Army.Count; i++)
        {
            if (_ourHand.Army[i].name == "Крестьянин")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                positionsOfImages[i].gameObject.SetActive(true);
                if (positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>() != null)
                {
                    positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>().gameObject.SetActive(false);
                    showKickbutton.ShowControls = false;
                }
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Маг")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                positionsOfImages[i].gameObject.SetActive(true);
                if (positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>() != null)
                {
                    positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>().gameObject.SetActive(false);
                    showKickbutton.ShowControls = false;
                }
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Рыцарь")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                positionsOfImages[i].gameObject.SetActive(true);
                if (positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>() != null)
                {
                    positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>().gameObject.SetActive(false);
                    showKickbutton.ShowControls = false;
                }
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Всадник")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                positionsOfImages[i].gameObject.SetActive(true);
                if (positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>() != null)
                {
                    positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>().gameObject.SetActive(false);
                    showKickbutton.ShowControls = false;
                }
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Лучник")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                positionsOfImages[i].gameObject.SetActive(true);
                if (positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>() != null)
                {
                    positionsOfImages[i].gameObject.GetComponentInChildren<Buttons>().gameObject.SetActive(false);
                    showKickbutton.ShowControls = false;
                }
                Debug.Log("create");
            }

            if (positionsOfImages[i].image.sprite == null)
            {
                Debug.Log("false");
                positionsOfImages[i].enabled = false;
            }
            Debug.Log("зашло");
        }

        for (int i = 0; i < positionsOfImages.Length; i++)
        {
            if (positionsOfImages[i].image.sprite == null)
            {
                Debug.Log("false");
                positionsOfImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < positionsOfImages.Length; i++)
        {
            positionsOfImages[i].gameObject.SetActive(true);
            positionsOfImages[i].image.sprite = null;
        }
        
        _canvas.gameObject.SetActive(false);
        toBattleButton.gameObject.SetActive(true);
    }

    public void Kick(Button button)
    {
        _ourHand.KickHero(button);
    }
}
