using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : MonoBehaviour
{
    public Character.Classes _character;

    public int Health;
    public int Damage;
    public int Speed;
    public int Count = 0;

    private void Awake()
    {
        if (_character == Character.Classes.knight)
        {
            Health = 32;
            Damage = 10;
            Speed = 3;
        }

        if (_character == Character.Classes.peasant)
        {
            Health = 5;
            Damage = 1;
            Speed = 2;
        }
        
        if (_character == Character.Classes.mage)
        {
            Health = 23;
            Damage = 18;
            Speed = 2;
        }
        
        if (_character == Character.Classes.archer)
        {
            Health = 20;
            Damage = 15;
            Speed = 2;
        }
        
        if (_character == Character.Classes.horseman)
        {
            Health = 130;
            Damage = 29;
            Speed = 5;
        }
        
        if (_character == Character.Classes.zombie)
        {
            Health = 31;
            Damage = 9;
            Speed = 2;
        }
        
        if (_character == Character.Classes.skeleton)
        {
            Health = 12;
            Damage = 4;
            Speed = 2;
        }
        
        if (_character == Character.Classes.necromancer)
        {
            Health = 140;
            Damage = 30;
            Speed = 2;
        }
        
        if (_character == Character.Classes.blackKnight)
        {
            Health = 180;
            Damage = 33;
            Speed = 2;
        }
    }
}
