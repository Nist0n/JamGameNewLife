using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class Barracks : MonoBehaviour
    {
        [SerializeField] private Button trainButton; 
        [SerializeField] private Button researchButton; 
        [SerializeField] private TMP_Dropdown racesDropdown;
        [SerializeField] private TMP_Dropdown unitsDropdown;
        
        [SerializeField] private Slider unitsCountSlider;
        [SerializeField] private TMP_Text unitsCountText;

        [SerializeField] private List<TMP_Dropdown.OptionData> humanOptions = new();
        [SerializeField] private List<TMP_Dropdown.OptionData> gnomeOptions = new();
        [SerializeField] private List<TMP_Dropdown.OptionData> orcOptions = new();

        [SerializeField] private GameObject addUnitControls;

        private OurHand _ourHand;

        private bool _controlsShown;
        private bool _racesShown;

        private string _selectedUnit = "Knight";

        private void Start()
        {
            _ourHand = FindObjectOfType<OurHand>();
        }

        public void ToggleControls()
        {
            _controlsShown = !_controlsShown;
            trainButton.gameObject.SetActive(_controlsShown);
            researchButton.gameObject.SetActive(_controlsShown);
            
            _racesShown = false;
            racesDropdown.gameObject.SetActive(false);
            
            unitsDropdown.gameObject.SetActive(false);
            
            addUnitControls.gameObject.SetActive(false);
        }

        public void ToggleRaceDropdown()
        {
            _racesShown = !_racesShown;
            racesDropdown.gameObject.SetActive(_racesShown);
            
            unitsDropdown.gameObject.SetActive(_racesShown);
            
            addUnitControls.gameObject.SetActive(false);
            
            addUnitControls.gameObject.SetActive(_racesShown);
        }

        public void OpenUnitsDropdown()
        {
            int pickedEntryIndex = racesDropdown.value;
            string selectedRace = racesDropdown.options[pickedEntryIndex].text;

            unitsDropdown.ClearOptions();
            switch (selectedRace)
            {
                case "Люди":
                    unitsDropdown.AddOptions(humanOptions);
                    break;
                case "Гномы":
                    unitsDropdown.AddOptions(gnomeOptions);
                    break;
                case "Орки":
                    unitsDropdown.AddOptions(orcOptions);
                    break;
            }
            
            unitsDropdown.gameObject.SetActive(true);
        }

        public void SelectUnit()
        {
            int pickedEntryIndex = unitsDropdown.value;
            _selectedUnit = unitsDropdown.options[pickedEntryIndex].text;
        }

        public void TrainUnits()
        {
            List<string> units = new();
            int count = Convert.ToInt32(unitsCountSlider.value);

            for (int i = 0; i < count; i++)
            {
                units.Add(_selectedUnit);
                _ourHand.AddCountOfUnits(_selectedUnit, count);
                Debug.Log(units[i]);
            }
        }
        
        public void ChangeUnitCount()
        {
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
        }

        public void IncreaseUnitCount()
        {
            unitsCountSlider.value++;
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
        }

        public void DecreaseUnitCount()
        {
            unitsCountSlider.value--;
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
        }
    }
}