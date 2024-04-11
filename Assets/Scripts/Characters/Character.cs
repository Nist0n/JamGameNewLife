using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character : MonoBehaviour
{  
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

    private void Start()
    {
        _heroChose = FindObjectOfType<HeroChose>();
        _class = GetComponent<Class>();

        Health = _class.Health;
        Damage = _class.Damage;
        Speed = _class.Speed;
    }

    public void GetDamage(int damageNum)
    {
        Health -= damageNum;
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
    }
}
