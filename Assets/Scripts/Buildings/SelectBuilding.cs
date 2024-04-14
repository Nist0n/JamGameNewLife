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
        [SerializeField] private Button townHallButton;
        [SerializeField] private Button barracksButton;
        
        [SerializeField] private GameObject addUnitControls;
        [SerializeField] private GameObject researchControls;

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
                    addUnitControls.SetActive(false);
                    researchControls.SetActive(false);
                    Barracks.RacesShown = false;
                    Barracks.ResearchRacesShown = false;
                    if (hit.collider.gameObject.CompareTag("Building"))
                    {
                        AudioManager.instance.PlaySFX("Click");
                        
                        _selectedBuilding = hit.collider.gameObject;
                        switch (_selectedBuilding.name)
                        {
                            case "Mine":
                                mineSelectionCircle.SetActive(true);
                                townHallSelectionCircle.SetActive(false);
                                barracksSelectionCircle.SetActive(false);
                                
                                ToggleButton(mineButton, true);
                                ToggleButton(townHallButton, false);
                                ToggleButton(barracksButton, false);
                                break;
                            case "TownHall":
                                mineSelectionCircle.SetActive(false);
                                townHallSelectionCircle.SetActive(true);
                                barracksSelectionCircle.SetActive(false);
                                
                                ToggleButton(townHallButton, true);
                                ToggleButton(mineButton, false);
                                ToggleButton(barracksButton, false);
                                
                                // Barracks.RacesShown = true;
                                // Barracks.ResearchRacesShown = true;
                                break;
                            case "Barracks":
                                mineSelectionCircle.SetActive(false);
                                townHallSelectionCircle.SetActive(false);
                                barracksSelectionCircle.SetActive(true);
                                
                                ToggleButton(barracksButton, true);
                                ToggleButton(townHallButton, false);
                                ToggleButton(mineButton, false);
                                break;
                        }
                    }
                    else
                    {
                        mineSelectionCircle.SetActive(false);
                        townHallSelectionCircle.SetActive(false);
                        barracksSelectionCircle.SetActive(false);

                        ToggleButton(mineButton, false);
                        ToggleButton(townHallButton, false);
                        ToggleButton(barracksButton, false);
                    }
                }
            }
        }

        private void ToggleButton(Button button, bool toggle)
        {
            button.interactable = toggle;
            button.GetComponent<Image>().enabled = toggle;
            
            if (button == barracksButton && toggle)
            {
                for (int i = 0; i < 3; i++)
                {
                    button.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            else
            {
                foreach (Transform child in button.transform)
                {
                    child.gameObject.SetActive(toggle);
                }
            }
        }
    }
}
