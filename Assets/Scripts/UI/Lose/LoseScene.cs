using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScene : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    public void LoseGame()
    {
        LeanTween.scale(canvas, new Vector3(1.5f, 1.5f, 1.5f), 2).setDelay(.5f).setEase(LeanTweenType.easeOutElastic);
    }
}
