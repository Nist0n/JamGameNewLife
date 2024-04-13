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

    public void OpenMenu()
    {
        for (int i = 0; i < _ourHand.Army.Count; i++)
        {
            if (_ourHand.Army[i].name == "Peasant")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
            }
            
            if (_ourHand.Army[i].name == "Mage")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
            }
            
            if (_ourHand.Army[i].name == "Knight")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
            }
            
            if (_ourHand.Army[i].name == "Horseman")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
            }
            
            if (_ourHand.Army[i].name == "Archer")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
            }
        }
        
        _canvas.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        _canvas.gameObject.SetActive(false);
    }
}
