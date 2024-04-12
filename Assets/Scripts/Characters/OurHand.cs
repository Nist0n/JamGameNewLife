using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurHand : MonoBehaviour
{
    public List<GameObject> Army;
    public bool IsFull = false;

    [SerializeField] private GameObject _peasant;
    [SerializeField] private GameObject _knight;
    
    private static OurHand instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Army.Count >= 5)
        {
            IsFull = true;
        }
        else
        {
            IsFull = false;
        }
    }

    public void AddCountOfUnits(string unit, int count)
    {
        if (unit == "Peasant")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Peasant")
                {
                    ch.GetComponent<Class>().Count += count;
                    temp++;
                }
            }

            if (temp == 0)
            {
                Army.Add(_peasant);
                foreach (var ch in Army)
                {
                    if (ch.name == "Peasant")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Knight")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Knight")
                {
                    ch.GetComponent<Class>().Count += count;
                    Debug.Log(ch.GetComponent<Class>().Count);
                    temp++;
                }
            }

            if (temp == 0)
            {
                Army.Add(_knight);
                foreach (var ch in Army)
                {
                    if (ch.name == "Knight")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
    }
}
