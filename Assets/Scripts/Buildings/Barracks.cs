using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class Barracks : MonoBehaviour
    {
        [SerializeField] private Button trainButton; 
        [SerializeField] private Button researchButton; 
        [SerializeField] private Dropdown racesDropdown;
        [SerializeField] private Dropdown unitsDropdown;
        [SerializeField] private Dropdown researchRacesDropdown;
        [SerializeField] private Dropdown researchUnitsDropdown;
        
        [SerializeField] private Slider unitsCountSlider;
        [SerializeField] private TMP_Text unitsCountText;
        [SerializeField] private TMP_Text coinsCountText;
        [SerializeField] private TMP_Text addedCoinsText;
        [SerializeField] private TMP_Text leadershipCountText;
        [SerializeField] private TMP_Text sumText;
        // [SerializeField] private TMP_Text leadershipCheckoutText;

        [SerializeField] private List<Dropdown.OptionData> humanOptions = new();
        [SerializeField] private List<Dropdown.OptionData> gnomeOptions = new();
        [SerializeField] private List<Dropdown.OptionData> orcOptions = new();

        [SerializeField] private GameObject addUnitControls;

        [SerializeField] private Button acceptButton;
        
        [SerializeField] private Button addUnitButton;
        [SerializeField] private Button removeUnitButton;

        private OurHand _ourHand;

        private bool _controlsShown;
        public static bool RacesShown;
        public static bool ResearchRacesShown;

        private string _selectedUnit = "Peasant";

        private int _coins;
        private int _leadership;

        private int _unitCoinsCost = 10;
        private int _maxUnitsOfType;

        private Dictionary<string, int> _units = new();
        
        private void Start()
        {
            _ourHand = FindObjectOfType<OurHand>();
            _leadership = PlayerPrefs.GetInt(PlayerData.LeadershipData);
            _units = _ourHand.Units;
            // TODO: change leadership according to saved units data
            _maxUnitsOfType = _leadership / 5;
        }

        private void Update()
        {
            _coins = Convert.ToInt32(coinsCountText.text);
            _leadership = Convert.ToInt32(leadershipCountText.text);

            unitsCountSlider.maxValue = Math.Min(_maxUnitsOfType, _coins / _unitCoinsCost);

            addUnitButton.interactable = unitsCountSlider.value != unitsCountSlider.maxValue;
            removeUnitButton.interactable = unitsCountSlider.value != unitsCountSlider.minValue;
            unitsCountSlider.interactable = unitsCountSlider.maxValue != 0;
            
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
            
            acceptButton.gameObject.SetActive(unitsCountSlider.value != 0);

            sumText.text = (unitsCountSlider.value * _unitCoinsCost).ToString(CultureInfo.InvariantCulture);
            // leadershipCheckoutText.text = (unitsCountSlider.value * _unitCoinsCost).ToString(CultureInfo.InvariantCulture);
            
            switch (_selectedUnit)
            {
                case "Knight":
                    _unitCoinsCost = 70;
                    _maxUnitsOfType = _leadership / 35;
                    break;
                case "Archer":
                    _unitCoinsCost = 100;
                    _maxUnitsOfType = _leadership / 50;
                    break;
                case "Mage":
                    _unitCoinsCost = 100;
                    _maxUnitsOfType = _leadership / 50;
                    break;
                case "Horseman":
                    _unitCoinsCost = 800;
                    _maxUnitsOfType = _leadership / 180;
                    break;
                default:
                    _unitCoinsCost = 10;
                    _maxUnitsOfType = _leadership / 5;
                    break;
            }

            if (_units.TryGetValue(_selectedUnit, out var unit))
            {
                _maxUnitsOfType -= unit;
            }
        }
        
        public void ToggleControls()
        {
            _controlsShown = !_controlsShown;
            trainButton.gameObject.SetActive(_controlsShown);
            researchButton.gameObject.SetActive(_controlsShown);
            
            RacesShown = false;
            racesDropdown.gameObject.SetActive(false);
            unitsDropdown.gameObject.SetActive(false);
            researchRacesDropdown.gameObject.SetActive(false);
            researchUnitsDropdown.gameObject.SetActive(false);
            addUnitControls.gameObject.SetActive(false);
        }

        public void ToggleRaceDropdown()
        {
            RacesShown = !RacesShown;
            racesDropdown.gameObject.SetActive(RacesShown);
            
            unitsDropdown.gameObject.SetActive(RacesShown);
            
            addUnitControls.gameObject.SetActive(false);
            
            addUnitControls.gameObject.SetActive(RacesShown);
            
            ResearchRacesShown = false;
            researchRacesDropdown.gameObject.SetActive(ResearchRacesShown);
            researchUnitsDropdown.gameObject.SetActive(ResearchRacesShown);
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
            int count = Convert.ToInt32(unitsCountSlider.value);

            if (_units.ContainsKey(_selectedUnit))
            {
                _units[_selectedUnit] += count;
            }
            else
            {
                _units.Add(_selectedUnit, count);  
            }
            
            _ourHand.AddCountOfUnits(_selectedUnit, count);

            int coinsSpent = _unitCoinsCost * count;
            _coins -= coinsSpent;
            PlayerData.UpdateCoins(_coins);
            
            StartCoroutine(ShowMoneySpent(coinsSpent));
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

        private IEnumerator ShowMoneySpent(int coinsSpent)
        {
            addedCoinsText.gameObject.SetActive(true);
            addedCoinsText.text = "-" + coinsSpent;

            yield return new WaitForSeconds(3f);
            
            addedCoinsText.gameObject.SetActive(false);
        }

        public void ToggleResearchDropdown()
        {
            RacesShown = false;
            racesDropdown.gameObject.SetActive(RacesShown);
            unitsDropdown.gameObject.SetActive(RacesShown);
            addUnitControls.gameObject.SetActive(RacesShown);
            
            ResearchRacesShown = !ResearchRacesShown;
            researchRacesDropdown.gameObject.SetActive(ResearchRacesShown);
            
            researchUnitsDropdown.gameObject.SetActive(ResearchRacesShown);
        }
        
        public void SelectResearchRace()
        {
            int pickedEntryIndex = researchRacesDropdown.value;
            string selectedRace = researchRacesDropdown.options[pickedEntryIndex].text;
            
            researchUnitsDropdown.ClearOptions();
            switch (selectedRace)
            {
                case "Люди":
                    researchUnitsDropdown.AddOptions(humanOptions);
                    break;
                case "Гномы":
                    researchUnitsDropdown.AddOptions(gnomeOptions);
                    break;
                case "Орки":
                    researchUnitsDropdown.AddOptions(orcOptions);
                    break;
            }
            
            researchUnitsDropdown.gameObject.SetActive(true);
        }
    }
}