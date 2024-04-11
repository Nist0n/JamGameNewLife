using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ourHand;
    [SerializeField] private List<Transform> _pos;

    public bool IsStarted = false;

    private void Awake()
    {
        for (int i = 0; i < _ourHand.Count; i++)
        {
            Debug.Log("f");
            Instantiate(_ourHand[i], _pos[i]);
        }

        IsStarted = true;
    }
}
