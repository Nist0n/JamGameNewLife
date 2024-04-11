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
        
        private int _mineLevel;
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
            _mineLevel = PlayerPrefs.GetInt(MineLevel);
            levelText.text = "Level" + _mineLevel;
            StartCoroutine(MineMoney(_timeBetweenPay));
            CheckMineLevel();
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
            _mineLevel++;
            levelText.text = "Level " + _mineLevel;
            PlayerPrefs.SetInt(MineLevel, _mineLevel);
            
            CheckMineLevel();
            
            switch (_mineLevel)
            {
                case 1:
                    _payRate = 5;
                    _mineVaultLimit = 50;
                    break;
                case 2:
                    _payRate = 7;
                    _mineVaultLimit = 70;
                    break;
                case 3:
                    _payRate = 9;
                    _mineVaultLimit = 90;
                    break;
            }
        }
        
        public void CollectFromMine()
        {
            StartCoroutine(ShowAddedCoins());
            _coins += _addedCoins;
            _addedCoins = 0;
            collectButton.interactable = false;
            PlayerData.AddCoins(_coins);
        }
        
        private IEnumerator MineMoney(float delay)
        {
            while (_mineExists && _addedCoins < _mineVaultLimit)
            {
                _addedCoins += _payRate;
                collectButton.interactable = true;
                yield return new WaitForSeconds(delay);
            }
        }

        private void CheckMineLevel()
        {
            if (_mineLevel == 3)
            {
                upgradeButton.gameObject.SetActive(false);
            }
        }

        IEnumerator ShowAddedCoins()
        {
            addedCoinsText.gameObject.SetActive(true);
            addedCoinsText.text = "+" + _addedCoins;

            yield return new WaitForSeconds(3f);
            
            addedCoinsText.gameObject.SetActive(false);
        }
    }
}