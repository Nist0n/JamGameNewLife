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

        [SerializeField] private List<Dropdown.OptionData> humanOptions = new();
        [SerializeField] private List<Dropdown.OptionData> undeadOptions = new();

        [SerializeField] private GameObject addUnitControls;

        [SerializeField] private Button acceptButton;
        
        [SerializeField] private Button addUnitButton;
        [SerializeField] private Button removeUnitButton;
        
        [SerializeField] private GameObject researchControls;
        [SerializeField] private TMP_Text researchCoinsCheckout;
        [SerializeField] private Button acceptResearchButton;

        private OurHand _ourHand;

        private bool _controlsShown;
        public static bool RacesShown;
        public static bool ResearchRacesShown;

        private string _selectedUnit = "Крестьянин";

        private int _coins;
        private int _leadership;

        private int _unitCoinsCost = 10;
        private int _maxUnitsOfType;

        private Dictionary<string, int> _units = new();

        private int _researchCost;
        private string _researchedUnit = "Маг";

        private int _levelRequired = 100;

        public static List<string> ResearchUnitKeys = new()
            { "Маг", "Всадник", "Скелет", "Зомби", "Некромант", "Тёмный Рыцарь" };

        private readonly List<Dropdown.OptionData> _starterOptions = new()
            { new("Крестьянин"), new("Рыцарь"), new("Лучник") };
        
        private void Start()
        {
            _ourHand = FindObjectOfType<OurHand>();
            _leadership = PlayerPrefs.GetInt(PlayerData.LeadershipData);
            _units = _ourHand.Units;
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
            
            unitsCountText.gameObject.SetActive(unitsCountSlider.maxValue != 0);
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
            
            acceptButton.gameObject.SetActive(unitsCountSlider.value != 0 && !_ourHand.IsFull);
            
            // addUnitButton.gameObject.SetActive(unitsCountSlider.maxValue != 0);
            // removeUnitButton.gameObject.SetActive(unitsCountSlider.maxValue != 0);
            // unitsCountSlider.gameObject.GetComponentInChildren<Image>().enabled = unitsCountSlider.maxValue != 0;
            
            sumText.gameObject.SetActive(unitsCountSlider.maxValue != 0);
            sumText.text = (unitsCountSlider.value * _unitCoinsCost).ToString(CultureInfo.InvariantCulture);
            
            switch (_selectedUnit)
            {
                case "Рыцарь":
                    _unitCoinsCost = 70;
                    _maxUnitsOfType = _leadership / 35;
                    break;
                case "Лучник":
                    _unitCoinsCost = 100;
                    _maxUnitsOfType = _leadership / 50;
                    break;
                case "Маг":
                    _unitCoinsCost = 100;
                    _maxUnitsOfType = _leadership / 50;
                    break;
                case "Всадник":
                    _unitCoinsCost = 800;
                    _maxUnitsOfType = _leadership / 180;
                    break;
                case "Скелет":
                    _unitCoinsCost = 20;
                    _maxUnitsOfType = _leadership / 12;
                    break;
                case "Зомби":
                    _unitCoinsCost = 60;
                    _maxUnitsOfType = _leadership / 30;
                    break;
                case "Некромант":
                    _unitCoinsCost = 600;
                    _maxUnitsOfType = _leadership / 200;
                    break;
                case "Тёмный Рыцарь":
                    _unitCoinsCost = 1000;
                    _maxUnitsOfType = _leadership / 150;
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

            acceptResearchButton.interactable = _researchCost != 0 && _researchedUnit != string.Empty && _coins >= _researchCost && PlayerPrefs.GetInt("numOfLevel") >= _levelRequired;
            
            switch (_researchedUnit)
            {
                case "Маг":
                    _levelRequired = 3;
                    _researchCost = 3000;
                    break;
                case "Всадник":
                    _levelRequired = 5;
                    _researchCost = 4000;
                    break;
                case "Скелет":
                    _levelRequired = 7;
                    _researchCost = 7000;
                    break;
                case "Зомби":
                    _levelRequired = 7;
                    _researchCost = 8000;
                    break;
                case "Некромант":
                    _levelRequired = 8;
                    _researchCost = 12000;
                    break;
                case "Тёмный Рыцарь":
                    _levelRequired = 9;
                    _researchCost = 15000;
                    break;
                default:
                    _researchCost = 0;
                    break;
            }
            
            researchCoinsCheckout.text = _researchCost.ToString();

            researchUnitsDropdown.gameObject.SetActive(researchUnitsDropdown.options.Count != 0 && researchRacesDropdown.gameObject.activeSelf);
            unitsDropdown.gameObject.SetActive(unitsDropdown.options.Count != 0 && racesDropdown.gameObject.activeSelf);
            
            researchControls.SetActive(researchUnitsDropdown.options.Count > 0 && ResearchRacesShown);
            
            _ourHand.UpdateHand();
        }
        
        public void ToggleControls()
        {
            _controlsShown = !_controlsShown;
            trainButton.gameObject.SetActive(_controlsShown && !_ourHand.IsFull);
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
            AudioManager.instance.PlaySFX("Click");
            RacesShown = !RacesShown;
            racesDropdown.gameObject.SetActive(RacesShown);
            
            unitsDropdown.ClearOptions();
            int pickedEntryIndex = racesDropdown.value;
            string selectedRace = racesDropdown.options[pickedEntryIndex].text;

            switch (selectedRace)
            {
                case "Люди":
                    unitsDropdown.AddOptions(_starterOptions);
                    unitsDropdown.AddOptions(GetResearchableUnits(humanOptions));
                    break;
                case "Нежить":
                    unitsDropdown.AddOptions(GetResearchableUnits(undeadOptions));
                    break;
            }
            
            unitsDropdown.gameObject.SetActive(RacesShown);
            
            addUnitControls.gameObject.SetActive(false);
            
            addUnitControls.gameObject.SetActive(RacesShown);
            
            ResearchRacesShown = false;
            researchRacesDropdown.gameObject.SetActive(ResearchRacesShown);
            researchUnitsDropdown.gameObject.SetActive(ResearchRacesShown);
            researchControls.SetActive(ResearchRacesShown);
            
            // int pickedUnit = unitsDropdown.value;
            // _selectedUnit = unitsDropdown.options[pickedUnit].text;
        }

        public void OpenUnitsDropdown()
        {
            AudioManager.instance.PlaySFX("Click");
            int pickedEntryIndex = racesDropdown.value;
            string selectedRace = racesDropdown.options[pickedEntryIndex].text;

            unitsDropdown.ClearOptions();
            switch (selectedRace)
            {
                case "Люди":
                    unitsDropdown.AddOptions(_starterOptions);
                    unitsDropdown.AddOptions(GetResearchableUnits(humanOptions));
                    break;
                case "Нежить":
                    unitsDropdown.AddOptions(GetResearchableUnits(undeadOptions));
                    break;
            }
            
            unitsDropdown.gameObject.SetActive(true);
        }

        public void SelectUnit()
        {
            AudioManager.instance.PlaySFX("Click");
            int pickedEntryIndex = unitsDropdown.value;
            _selectedUnit = unitsDropdown.options[pickedEntryIndex].text;
        }

        public void TrainUnits()
        {
            AudioManager.instance.PlaySFX("Click");
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
            AudioManager.instance.PlaySFX("Click");
            unitsCountSlider.value++;
            unitsCountText.text = unitsCountSlider.value.ToString(CultureInfo.CurrentCulture);
        }

        public void DecreaseUnitCount()
        {
            AudioManager.instance.PlaySFX("Click");
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
            AudioManager.instance.PlaySFX("Click");
            RacesShown = false;
            racesDropdown.gameObject.SetActive(RacesShown);
            unitsDropdown.gameObject.SetActive(RacesShown);
            addUnitControls.gameObject.SetActive(RacesShown);
            
            ResearchRacesShown = !ResearchRacesShown;
            researchRacesDropdown.gameObject.SetActive(ResearchRacesShown);
            researchControls.SetActive(ResearchRacesShown);
            
            researchUnitsDropdown.ClearOptions();
            int pickedEntryIndex = researchRacesDropdown.value;
            string selectedRace = researchRacesDropdown.options[pickedEntryIndex].text;
            
            switch (selectedRace)
            {
                case "Люди":
                    researchUnitsDropdown.AddOptions(GetResearchableUnits(humanOptions, 0));
                    break;
                case "Нежить":
                    researchUnitsDropdown.AddOptions(GetResearchableUnits(undeadOptions, 0));
                    break;
            }
            
            researchUnitsDropdown.gameObject.SetActive(ResearchRacesShown);
        }
        
        public void SelectResearchRace()
        {
            AudioManager.instance.PlaySFX("Click");
            int pickedEntryIndex = researchRacesDropdown.value;
            string selectedRace = researchRacesDropdown.options[pickedEntryIndex].text;
            
            researchUnitsDropdown.ClearOptions();
            switch (selectedRace)
            {
                case "Люди":
                    researchUnitsDropdown.AddOptions(GetResearchableUnits(humanOptions, 0));
                    break;
                case "Нежить":
                    researchUnitsDropdown.AddOptions(GetResearchableUnits(undeadOptions, 0));
                    break;
            }
            
            researchUnitsDropdown.gameObject.SetActive(true);

            if (researchUnitsDropdown.options.Count > 0)
            {
                int pickedResearchUnit = researchUnitsDropdown.value;
                _researchedUnit = researchUnitsDropdown.options[pickedResearchUnit].text;
            }
        }

        public void SelectResearchUnit()
        {
            AudioManager.instance.PlaySFX("Click");
            int pickedEntryIndex = researchUnitsDropdown.value;
            _researchedUnit = researchUnitsDropdown.options[pickedEntryIndex].text;
        }

        public void AcceptResearchUnit()
        {
            int pickedEntryIndex = researchUnitsDropdown.value;
            _researchedUnit = researchUnitsDropdown.options[pickedEntryIndex].text;
            
            AudioManager.instance.PlaySFX("Click");
            if (ResearchUnitKeys.Contains(_researchedUnit))
            {
                PlayerPrefs.SetInt(_researchedUnit, 1);
                _coins -= _researchCost;
                PlayerData.UpdateCoins(_coins);
                StartCoroutine(ShowMoneySpent(_researchCost));

                if (researchUnitsDropdown.options.Count == 1)
                {
                    researchUnitsDropdown.ClearOptions();
                }
                else
                {
                    var researchedOption = researchUnitsDropdown.options.Find(x => x.text == _researchedUnit);
                    List<Dropdown.OptionData> list = new();
                    foreach (var option in researchUnitsDropdown.options)
                    {
                        if (option != researchedOption)
                        {
                            list.Add(option);
                        }
                    }
                
                    researchUnitsDropdown.ClearOptions();
                    researchUnitsDropdown.AddOptions(list);
                }
                
                // unitsDropdown.options.Add(researchedOption);
            }
        }

        private List<Dropdown.OptionData> GetResearchableUnits(List<Dropdown.OptionData> raceList, int researched = 1)
        {
            List<Dropdown.OptionData> list = new();

            foreach (var unit in raceList)
            {
                if (ResearchUnitKeys.Contains(unit.text))
                {
                    int status = PlayerPrefs.GetInt(unit.text);
                    Debug.Log(unit.text);
                    Debug.Log(status);
                    if (status == researched)
                    {
                        Dropdown.OptionData option = new Dropdown.OptionData(unit.text);
                        list.Add(option);
                    }
                }
            }

            return list;
        }
    }
}