using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    private Camera _camera;

    private GameObject _selectedBuilding;

    [SerializeField] private GameObject mineSelectionCircle;
    [SerializeField] private GameObject townHallSelectionCircle;
    [SerializeField] private GameObject barracksSelectionCircle;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Building"))
                {
                    _selectedBuilding = hit.collider.gameObject;
                    switch (_selectedBuilding.name)
                    {
                        case "Mine":
                            mineSelectionCircle.SetActive(true);
                            townHallSelectionCircle.SetActive(false);
                            barracksSelectionCircle.SetActive(false);
                            break;
                        case "TownHall":
                            mineSelectionCircle.SetActive(false);
                            townHallSelectionCircle.SetActive(true);
                            barracksSelectionCircle.SetActive(false);
                            break;
                        case "Barracks":
                            mineSelectionCircle.SetActive(false);
                            townHallSelectionCircle.SetActive(false);
                            barracksSelectionCircle.SetActive(true);
                            break;
                    }
                    // _selectedBuilding.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(true);
                    Debug.Log(_selectedBuilding.name);
                }
                else
                {
                    mineSelectionCircle.SetActive(false);
                    townHallSelectionCircle.SetActive(false);
                    barracksSelectionCircle.SetActive(false);
                }
            }
        }
    }
}
