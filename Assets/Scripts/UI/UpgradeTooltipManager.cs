using Buildings;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UpgradeTooltipManager : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;
        
        private void Start()
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.position = Input.mousePosition;
        }

        public void SetAndShowTooltip(string buildingName)
        {
            transform.position = Input.mousePosition;
            gameObject.SetActive(true);
            
            int levelUpCost = 0;
            switch (buildingName)
            {
                case "Mine":
                    levelUpCost = Mine.LevelUpCost;
                    break;
                case "TownHall":
                    levelUpCost = TownHall.LevelUpCost;
                    break;
            }
            textComponent.text = "Cost: " + levelUpCost;
        }
        
        public void HideTooltip()
        {
            gameObject.SetActive(false);
            textComponent.text = string.Empty;
        }
    }
}