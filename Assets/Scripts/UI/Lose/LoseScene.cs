using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScene : MonoBehaviour
{
    [SerializeField] private GameObject loseObject;
    [SerializeField] private GameObject winObject;

    public void LoseGame()
    {
        LeanTween.scale(loseObject, new Vector3(1.5f, 1.5f, 1.5f), 2).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }
    
    public void WinGame()
    {
        LeanTween.scale(winObject, new Vector3(1.5f, 1.5f, 1.5f), 2).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }
}
