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
        AudioManager.instance.PlaySFX("Click");
        ShowControls = !ShowControls;
        kickButton.gameObject.SetActive(ShowControls);
    }
}
