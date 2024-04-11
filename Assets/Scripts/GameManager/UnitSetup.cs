using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ourHand;
    [SerializeField] private List<Transform> _pos;

    public bool IsStarted = false;
    
    private void Start()
    {
        for (int i = 0; i < _ourHand.Count; i++)
        {
            Debug.Log("f");
            Instantiate(_ourHand[i], _pos[i]);
        }
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        IsStarted = true;
    }
}
