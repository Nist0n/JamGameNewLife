using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowKickbutton : MonoBehaviour
{
    [SerializeField] private Button kickButton;
    
    private bool _showControls;

    public void ToggleControls()
    {
        _showControls = !_showControls;
        kickButton.gameObject.SetActive(_showControls);
    }
}
