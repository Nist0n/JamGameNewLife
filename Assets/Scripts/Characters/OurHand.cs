using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OurHand : MonoBehaviour
{
    public List<GameObject> Army;
    public Dictionary<string, int> Units = new();
    public bool IsFull;

    [SerializeField] private GameObject _peasant;
    [SerializeField] private GameObject _knight;
    [SerializeField] private GameObject _mage;
    [SerializeField] private GameObject _archer;
    [SerializeField] private GameObject _horseman;

    private const string SaveKey = "mainSave";
    
    private static OurHand instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
        if (unit == "Крестьянин")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Крестьянин")
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
                    if (ch.name == "Крестьянин")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Рыцарь")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Рыцарь")
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
                    if (ch.name == "Рыцарь")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Маг")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Маг")
                {
                    ch.GetComponent<Class>().Count += count;
                    temp++;
                }
            }

            if (temp == 0)
            {
                Army.Add(_mage);
                foreach (var ch in Army)
                {
                    if (ch.name == "Маг")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Лучник")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Лучник")
                {
                    ch.GetComponent<Class>().Count += count;
                    temp++;
                }
            }

            if (temp == 0)
            {
                Army.Add(_archer);
                foreach (var ch in Army)
                {
                    if (ch.name == "Лучник")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Всадник")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Всадник")
                {
                    ch.GetComponent<Class>().Count += count;
                    temp++;
                }
            }

            if (temp == 0)
            {
                Army.Add(_horseman);
                foreach (var ch in Army)
                {
                    if (ch.name == "Всадник")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        Save();
    }

    public void Load()
    {
        var data = SaveSystem.LoadArmy<ArmyData>(SaveKey);

        if (data != null)
        {
            this.Army = data.Army;
            foreach (var unit in Army)
            {
                int count = unit.GetComponent<Class>().Count;
                if (Units.ContainsKey(unit.name))
                {
                    Units[unit.name] += count;
                }
                else
                {
                    Units.Add(unit.name, count);  
                }
            }
        }
    }

    public void Save()
    {
        SaveSystem.Save(SaveKey, GetSaveSnapshot());
    }

    public void DeleteSave()
    {
        PlayerPrefs.SetString(SaveKey, String.Empty);
    }

    private ArmyData GetSaveSnapshot()
    {
        var data = new ArmyData()
        {
            Army = this.Army,
        };

        return data;
    }

    public void KickHero(Button button)
    {
        foreach (var unit in Army)
        {
            if (unit.name == button.image.sprite.name)
            {
                Army.Remove(unit);
                button.image.sprite = null;
                button.gameObject.GetComponentInChildren<Button>().gameObject.SetActive(false);
                button.gameObject.SetActive(false);
                break;
            }
        }
    }
}
