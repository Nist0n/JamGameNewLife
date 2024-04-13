using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSounds : MonoBehaviour
{
    private static DataSounds instance;
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
}
