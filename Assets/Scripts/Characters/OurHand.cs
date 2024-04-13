using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OurHand : MonoBehaviour
{
    public List<GameObject> Army;
    public Dictionary<string, int> Units = new();
    public bool IsFull = false;

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
        if (Units.ContainsKey(unit))
        {
            Units[unit] += count;
        }
        else
        {
            Units.Add(unit, count);  
        }
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
        
        if (unit == "Mage")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Mage")
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
                    if (ch.name == "Mage")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Archer")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Archer")
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
                    if (ch.name == "Archer")
                    {
                        ch.GetComponent<Class>().Count += count;
                    }
                }
            }
        }
        
        if (unit == "Horseman")
        {
            int temp = 0;
            foreach (var ch in Army)
            {
                if (ch.name == "Horseman")
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
                    if (ch.name == "Horseman")
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
