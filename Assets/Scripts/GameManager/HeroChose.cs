using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroChose : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick.GetComponent<GameObject>().CompareTag("hero"))
        {
            Console.WriteLine("gg");
        }
        Console.WriteLine("ggdfd");
    }
}
