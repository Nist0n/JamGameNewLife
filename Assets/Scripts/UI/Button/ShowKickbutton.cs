using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKickbutton : MonoBehaviour
{
    [SerializeField] private Button kickButton;
    
    public bool ShowControls;

    public void ToggleControls()
    {
        ShowControls = !ShowControls;
        kickButton.gameObject.SetActive(ShowControls);
    }
}
