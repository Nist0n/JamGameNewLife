using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateBattleButton : MonoBehaviour
{
    private OurHand _ourHand;
    void Start()
    {
        _ourHand = FindObjectOfType<OurHand>();
    }
    
    void Update()
    {
        if (_ourHand.Army.Count == 0)
        {
            this.gameObject.GetComponentInChildren<Button>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponentInChildren<Button>().enabled = true;
        }
    }
}
