using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class Mine : MonoBehaviour
    {
        private const string MineLevel = "MineLevel";

        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button collectButton;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text addedCoinsText;
        [SerializeField] private TMP_Text mineCoinsCountText;
        
        private int _mineLevel = 1;
        private int _levelUpCost = 30;
        private int _payRate = 5;
        private bool _mineExists = true;
        private float _timeBetweenPay = 5;
        private int _addedCoins;
        private int _mineVaultLimit = 50;

        private int _coins;

        private bool _mineControlsShown;
        
        private void Start()
        {
            _coins = PlayerPrefs.GetInt(PlayerData.CoinsData);
            if (PlayerPrefs.HasKey(MineLevel))
            {
                _mineLevel = PlayerPrefs.GetInt(MineLevel);
            }
            levelText.text = "Level " + _mineLevel;
            StartCoroutine(MineMoney(_timeBetweenPay));
        }

        private void FixedUpdate()
        {
            if (_mineLevel == 3)
            {
                upgradeButton.gameObject.SetActive(false);
            }

            upgradeButton.interactable = _coins >= _levelUpCost;

            UpdateMineValues();
            mineCoinsCountText.text = _addedCoins + "/" + _mineVaultLimit;
        }
        
        public void ToggleMineControls()
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
        
        public void UpgradeMine()
        {
            _coins -= _levelUpCost;
            PlayerData.UpdateCoins(_coins);
            
            _mineLevel++;
            levelText.text = "Level " + _mineLevel;
            PlayerPrefs.SetInt(MineLevel, _mineLevel);
        }
        
        public void CollectFromMine()
        {
            collectButton.interactable = false;
            StartCoroutine(ShowAddedCoins());
            _coins += _addedCoins;
            _addedCoins = 0;
            PlayerData.UpdateCoins(_coins);
        }

        private void UpdateMineValues()
        {
            switch (_mineLevel)
            {
                case 2:
                    _payRate = 7;
                    _mineVaultLimit = 70;
                    _levelUpCost = 60;
                    break;
                case 3:
                    _payRate = 9;
                    _mineVaultLimit = 90;
                    _levelUpCost = 90;
                    break;
            }
        }
        
        private IEnumerator MineMoney(float delay)
        {
            yield return new WaitForSeconds(3f);
            while (_mineExists && _addedCoins < _mineVaultLimit)
            {
                _addedCoins += _payRate;
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
    }
}