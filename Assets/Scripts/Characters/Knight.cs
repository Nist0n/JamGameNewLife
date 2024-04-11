using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public int Damage;
    [SerializeField] private int health;
    [SerializeField] private int speed;

    public void GetDamage(int damageNum)
    {
        health -= damageNum;
    }

    private void Update()
    {
        if (health <= 0)
        {
        }
    }
}
