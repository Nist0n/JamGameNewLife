using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SaveSystem
{
    public static void Save<T>(string key, T data)
    {
        string jsonDataString = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonDataString);
        PlayerPrefs.SetInt("firstSession", 2);
    }

    public static T LoadArmy<T>(string key) where T: new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            return new T();
        }
    }
}
