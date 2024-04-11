using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private int health;

    private HeroChose _heroChose;
    
    public int Speed;
    public int Damage;

    private void Start()
    {
        _heroChose = FindObjectOfType<HeroChose>();
    }

    public void GetDamage(int damageNum)
    {
        health -= damageNum;
    }

    private void Update()
    {
        if (health <= 0)
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
