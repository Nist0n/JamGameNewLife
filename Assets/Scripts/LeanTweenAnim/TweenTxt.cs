using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTxt : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float scaleSize;
    [SerializeField] LeanTweenType easeType;
    void Start()
    {
        LeanTween.scale(gameObject, transform.localScale * scaleSize, duration).setLoopPingPong();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DownSize();
    }
    void DownSize()
    {
        LeanTween.scale(gameObject, transform.localScale * 0, duration).setEase(easeType).setOnComplete(DestroyObject);
    }    
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
