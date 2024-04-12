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
        [SerializeField] private TMP_Dropdown racesDropdown;
        [SerializeField] private TMP_Dropdown unitsDropdown;
        
        [SerializeField] private Slider unitsCountSlider;
        [SerializeField] private TMP_Text unitsCountText;
        [SerializeField] private TMP_Text coinsCountText;
        [SerializeField] private TMP_Text addedCoinsText;
        [SerializeField] private TMP_Text leadershipCountText;

        [SerializeField] private List<TMP_Dropdown.OptionData> humanOptions = new();
        [SerializeField] private List<TMP_Dropdown.OptionData> gnomeOptions = new();
        [SerializeField] private List<TMP_Dropdown.OptionData> orcOptions = new();

        [SerializeField] private GameObject addUnitControls;

        [SerializeField] private Button acceptButton;

        private OurHand _ourHand;

        private bool _controlsShown;
        private bool _racesShown;

        private string _selectedUnit = "Peasant";

        private int _coins;
        private int _leadership;

        private int _unitCoinsCost = 10;
        private int _maxUnitsOfType;

        private void Start()
        {
            _ourHand = FindObjectOfType<OurHand>();
            _leadership = PlayerPrefs.GetInt(PlayerData.LeadershipData);
            _maxUnitsOfType = _leadership / 5;
        }

        private void Update()
        {
            _coins = Convert.ToInt32(coinsCountText.text);
            _leadership = Convert.ToInt32(leadershipCountText.text);

            unitsCountSlider.maxValue = Math.Min(_maxUnitsOfType, _coins / _unitCoinsCost);
            
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
            
            acceptButton.gameObject.SetActive(unitsCountSlider.value != 0);
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
            
            switch (_selectedUnit)
            {
                case "Knight":
                    _unitCoinsCost = 70;
                    _maxUnitsOfType = _leadership / 35;
                    break;
                case "Archer":
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
    }
}