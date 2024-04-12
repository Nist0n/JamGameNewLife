using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Text _text;
    
    private HeroChose _heroChose;
    private Class _class;

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
        Count -= (damageNum * countNum) / _class.Health;
        _class.Count -= (damageNum * countNum) / _class.Health;
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
                _heroChose.HeroesGroup.Remove(gameObject);
            }
            catch
            {
                Exception exception;
            }
        }
        
        _text.text = Convert.ToString(Count);
    }
}
