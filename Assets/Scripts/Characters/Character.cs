using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Text _text;
    private OurHand _ourHand;
    private HeroChose _heroChose;
    private Class _class;

    public GameObject Circle;

    public enum Classes
    {
        knight,
        archer,
        mage,
        peasant,
        horseman
    }
    
    public int Health;
    public int Speed;
    public int Damage;
    public int Count;

    private void Start()
    {
        _heroChose = FindObjectOfType<HeroChose>();
        _class = GetComponent<Class>();
        _text = GetComponentInChildren<Text>();
        _ourHand = FindObjectOfType<OurHand>();
        
        Damage = _class.Damage;
        Speed = _class.Speed;
        Count = _class.Count;
        Speed = _class.Speed;
        Health = _class.Health * Count;
        _text.text = Convert.ToString(Count);
    }

    public void GetDamage(int damageNum, int countNum)
    {
        Health -= damageNum * countNum;
        Debug.Log((damageNum * countNum)/_class.Health);
        
        if (Count - (damageNum * countNum) / _class.Health >= 0)
        {
            Count -= (damageNum * countNum) / _class.Health;
        }
        else
        {
            Count = 0;
        }
        
        if (_class.Count - (damageNum * countNum) / _class.Health >= 0)
        {
            _class.Count -= (damageNum * countNum) / _class.Health;
        }
        else
        {
            _class.Count = 0;
        }
        
        if (gameObject.CompareTag("hero"))
        {
            foreach (var unit in _ourHand.Army)
            {
                if (_class._character == unit.GetComponent<Class>()._character)
                {
                    unit.GetComponent<Class>().Count = _class.Count;
                }
            }
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            try
            {
                _heroChose.List.Remove(gameObject);
                _heroChose.EnemiesGroup.Remove(gameObject);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (gameObject.CompareTag("hero"))
            {
                foreach (var unit in _ourHand.Army)
                {
                    if (gameObject.GetComponent<Class>()._character == unit.GetComponent<Class>()._character)
                    {
                        _ourHand.Army.Remove(unit);
                        break;
                    }
                }
            }
        }
        
        _text.text = Convert.ToString(Count);
    }
}
