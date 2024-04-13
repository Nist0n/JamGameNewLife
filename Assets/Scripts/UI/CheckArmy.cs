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
        _canvas.gameObject.SetActive(true);
        
        for (int i = 0; i < _ourHand.Army.Count; i++)
        {
            if (_ourHand.Army[i].name == "Peasant")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Mage")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Knight")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Horseman")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
                Debug.Log("create");
            }
            
            if (_ourHand.Army[i].name == "Archer")
            {
                positionsOfImages[i].image.sprite = _ourHand.Army[i].GetComponent<ImageOfUintChar>().Sprite;
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
        }
        
        _canvas.gameObject.SetActive(false);
    }
}
