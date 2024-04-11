using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanTweenController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) MenuAnimations();
    }
    public void MenuAnimations()
    {
        LeanTween.moveY(gameObject, target.position.y, duration).setEase(easeType).setDelay(1);
    }

}
