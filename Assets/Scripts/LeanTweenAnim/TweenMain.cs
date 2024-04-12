using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanTweenController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float duration; 
    [SerializeField] float delay;
    [SerializeField] LeanTweenType easeType;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) MenuFall();
    }
    private void MenuFall()
    {
        LeanTween.moveY(gameObject, target.position.y, duration).setDelay(delay).setEase(easeType);
    }
}    