using System;
using System.Collections;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class TownHall : Building
    {
        private const string TownHallLevel = "TownHallLevel";
        
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text addedCoinsText;
        [SerializeField] private TMP_Text coinsText;

        private int _level = 1;
        private int _levelUpCost = 30;
        private int _leadership = 100;

        private bool _controlsShown;

        private int _coins;

        private void Start()
        {
            if (PlayerPrefs.HasKey(TownHallLevel))
            {
                _level = PlayerPrefs.GetInt(TownHallLevel);
            }

            levelText.text = "Level: " + _level;
            _coins = PlayerPrefs.GetInt(PlayerData.CoinsData);
            
            UpdateValues();
        }

        private void FixedUpdate()
        {
            if (_level == 3)
            {
                upgradeButton.gameObject.SetActive(false);
            }

            _coins = Convert.ToInt32(coinsText.text);
            upgradeButton.interactable = _coins >= _levelUpCost;
        }

        public override void ToggleControls()
        {
            _controlsShown = !_controlsShown;
            if (_level < 3)
            {
                upgradeButton.gameObject.SetActive(_controlsShown);
            }
        }

        public override void Upgrade()
        {
            _coins -= _levelUpCost;
            PlayerData.UpdateCoins(_coins);

            _level++;
            levelText.text = "Level: " + _level;
            PlayerPrefs.SetInt(TownHallLevel, _level);

            StartCoroutine(ShowMoneySpent());
            
            UpdateValues();
        }

        protected override void UpdateValues()
        {
            switch (_level) 
            {
                case 2:
                    _leadership = 200;
                    _levelUpCost = 60;
                    break;
                case 3:
                    _leadership = 300;
                    _levelUpCost = 90;
                    break;
            }
            
            PlayerData.UpdateLeadership(_leadership);
        }
        private IEnumerator ShowMoneySpent()
        {
            addedCoinsText.gameObject.SetActive(true);
            addedCoinsText.text = "-" + _levelUpCost;

            yield return new WaitForSeconds(3f);
            
            addedCoinsText.gameObject.SetActive(false);
        }
    }
}