using System;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Buildings
{
    public class Mine : Building
    {
        private const string MineLevel = "MineLevel";

        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button collectButton;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text addedCoinsText;
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text mineCoinsCountText;
        
        private int _mineLevel = 1;
        public static int LevelUpCost = 30000;
        private int _payRate = 500;
        private bool _mineExists = true;
        private float _timeBetweenPay = 5;
        private int _addedCoins;
        private int _mineVaultLimit = 15000;

        private int _coins;

        private bool _mineControlsShown;

        private void Start()
        {
            _coins = PlayerPrefs.GetInt(PlayerData.CoinsData);
            if (PlayerPrefs.HasKey(MineLevel))
            {
                _mineLevel = PlayerPrefs.GetInt(MineLevel);
            }
            levelText.text = "Level: " + _mineLevel;

            StartCoroutine(MineMoney(_timeBetweenPay));
        }

        private void FixedUpdate()
        {
            _coins = Convert.ToInt32(coinsText.text);
            if (_mineLevel == 3)
            {
                upgradeButton.gameObject.SetActive(false);
            }

            upgradeButton.interactable = _coins >= LevelUpCost;

            UpdateValues();
            mineCoinsCountText.text = _addedCoins + "/" + _mineVaultLimit;
        }
        
        public override void ToggleControls()
        {
            _mineControlsShown = !_mineControlsShown;
            if (_mineControlsShown)
            {
                if (_mineLevel < 3)
                {
                    upgradeButton.gameObject.SetActive(true);
                }
                collectButton.gameObject.SetActive(true);
            }
            else
            {
                upgradeButton.gameObject.SetActive(false);
                collectButton.gameObject.SetActive(false);
            }
        }
        
        public override void Upgrade()
        {
            _coins -= LevelUpCost;
            PlayerData.UpdateCoins(_coins);
            
            _mineLevel++;
            levelText.text = "Level: " + _mineLevel;
            PlayerPrefs.SetInt(MineLevel, _mineLevel);

            StartCoroutine(ShowMoneySpent());
        }
        
        protected override void UpdateValues()
        {
            switch (_mineLevel)
            {
                case 2:
                    _payRate = 750;
                    _mineVaultLimit = 22500;
                    LevelUpCost = 45000;
                    break;
                case 3:
                    _payRate = 1000;
                    _mineVaultLimit = 30000;
                    LevelUpCost = 60000;
                    break;
            }
        }
        
        public void CollectFromMine()
        {
            collectButton.interactable = false;
            StartCoroutine(ShowAddedCoins());
            _coins += _addedCoins;
            _addedCoins = 0;
            PlayerData.UpdateCoins(_coins);
        }

        private IEnumerator MineMoney(float delay)
        {
            yield return new WaitForSeconds(3f);
            
            while (_mineExists)
            {
                if (_addedCoins + _payRate < _mineVaultLimit)
                {
                    _addedCoins += _payRate;
                }
                else
                {
                    _addedCoins = _mineVaultLimit;
                }
                collectButton.interactable = true;
                yield return new WaitForSeconds(delay);
            }
        }

        private IEnumerator ShowAddedCoins()
        {
            addedCoinsText.gameObject.SetActive(true);
            addedCoinsText.text = "+" + _addedCoins;

            yield return new WaitForSeconds(3f);
            
            addedCoinsText.gameObject.SetActive(false);
        }
        
        private IEnumerator ShowMoneySpent()
        {
            addedCoinsText.gameObject.SetActive(true);
            addedCoinsText.text = "-" + LevelUpCost;

            yield return new WaitForSeconds(3f);
            
            addedCoinsText.gameObject.SetActive(false);
        }
    }
}