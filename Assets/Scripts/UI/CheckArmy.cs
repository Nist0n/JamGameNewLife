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

    private void Start()
    {
        _ourHand = FindObjectOfType<OurHand>();
    }

    public void OpenMenu()
    {
        _canvas.gameObject.SetActive(true);
        
        for (int i = 0; i < _ourHand.Army.Count; i++)
        {
            if (_ourHand.Army[i].name == "Peasant")
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
            
            if (_ourHand.Army[i].name == "Mage")
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
            
            if (_ourHand.Army[i].name == "Knight")
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
            
            if (_ourHand.Army[i].name == "Horseman")
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
            
            if (_ourHand.Army[i].name == "Archer")
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
    }

    public void Kick(Button button)
    {
        _ourHand.KickHero(button);
    }
}
