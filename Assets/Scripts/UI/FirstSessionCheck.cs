using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstSessionCheck : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        if (PlayerPrefs.GetInt("firstSession") != 2)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
    }
}
