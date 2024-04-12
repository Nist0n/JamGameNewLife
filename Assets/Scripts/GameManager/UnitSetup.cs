using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSetup : MonoBehaviour
{
    [SerializeField] private List<GameObject> _ourHandTest;
    [SerializeField] private List<Transform> _pos;
    private OurHand _ourHand;

    public bool IsStarted = false;
    
    private void Start()
    {
        _ourHand = FindObjectOfType<OurHand>();
        for (int i = 0; i < _ourHand.Army.Count; i++)
        {
            Debug.Log("f");
            Instantiate(_ourHand.Army[i], _pos[i]);
        }
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        IsStarted = true;
    }
}
