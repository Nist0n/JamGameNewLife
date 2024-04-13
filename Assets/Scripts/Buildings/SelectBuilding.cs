using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buildings
{
    public class SelectBuilding : MonoBehaviour
    {
        private Camera _camera;

        private GameObject _selectedBuilding;

        [SerializeField] private GameObject mineSelectionCircle;
        [SerializeField] private GameObject townHallSelectionCircle;
        [SerializeField] private GameObject barracksSelectionCircle;
        
        [SerializeField] private Button mineButton;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                
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
                                
                                ActivateButton(mineButton);
                                break;
                            case "TownHall":
                                mineSelectionCircle.SetActive(false);
                                townHallSelectionCircle.SetActive(true);
                                barracksSelectionCircle.SetActive(false);
                                
                                DeactivateButton(mineButton);
                                break;
                            case "Barracks":
                                mineSelectionCircle.SetActive(false);
                                townHallSelectionCircle.SetActive(false);
                                barracksSelectionCircle.SetActive(true);
                                
                                DeactivateButton(mineButton);
                                break;
                        }
                    }
                    else
                    {
                        mineSelectionCircle.SetActive(false);
                        townHallSelectionCircle.SetActive(false);
                        barracksSelectionCircle.SetActive(false);

                        DeactivateButton(mineButton);
                    }
                }
            }
        }

        private void ActivateButton(Button button)
        {
            button.interactable = true;
            button.GetComponent<Image>().enabled = true;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        
        private void DeactivateButton(Button button)
        {
            button.interactable = false;
            button.GetComponent<Image>().enabled = false;
            foreach (Transform child in button.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
