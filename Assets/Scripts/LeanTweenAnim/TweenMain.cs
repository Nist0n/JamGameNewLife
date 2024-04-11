using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanTweenController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;
    [SerializeField] float scaleSize;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) MenuFall();
    }
    private void MenuFall()
    {
        LeanTween.moveY(gameObject, target.position.y, duration).setDelay(1).setEase(easeType);
    }
    private void OnMouseEnter()
    {
        LeanTween.scale(gameObject, transform.localScale * scaleSize, 1);
    }
    private void OnMouseExit()
    {
    }
}
