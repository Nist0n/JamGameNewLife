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
    }
}
