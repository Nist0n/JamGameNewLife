using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTxt : MonoBehaviour
{
    [SerializeField] float duration;
    void Start()
    {
        LeanTween.scale(gameObject, transform.localScale * 2, duration).setLoopPingPong();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) DestroyObject();
    }
    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
