using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitTooltipManager : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;

        [SerializeField] private Dropdown unitsDropdown;
        
        private void Start()
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void SetAndShowTooltip()
        {
            transform.position = Input.mousePosition;
            gameObject.SetActive(true);
            
            int cost;
            int leadership;
            int pickedEntryIndex = unitsDropdown.value;
            string selectedUnit = unitsDropdown.options[pickedEntryIndex].text;

            switch (selectedUnit)
            {
                case "Knight":
                    cost = 70;
                    leadership = 35;
                    break;
                case "Archer":
                    cost = 100;
                    leadership = 50;
                    break;
                case "Mage":
                    cost = 100;
                    leadership = 50;
                    break;
                case "Horseman":
                    cost = 800;
                    leadership = 180;
                    break;
                default:
                    cost = 10;
                    leadership = 5;
                    break;
            }

            textComponent.text = "Cost: " + cost + "\n" + "Leadership: " + leadership;
        }
        
        public void HideTooltip()
        {
            gameObject.SetActive(false);
            textComponent.text = string.Empty;
        }
    }
}