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
        public static int LevelUpCost = 25000;
        private int _leadership = 0;

        private bool _controlsShown;

        private int _coins;

        private void Start()
        {
            if (PlayerPrefs.HasKey(TownHallLevel))
            {
                _level = PlayerPrefs.GetInt(TownHallLevel);
            }

            levelText.text = "Уровень: " + _level;
            _coins = PlayerPrefs.GetInt(PlayerData.CoinsData);
        }

        private void FixedUpdate()
        {
            if (_level == 4)
            {
                upgradeButton.gameObject.SetActive(false);
            }

            _coins = Convert.ToInt32(coinsText.text);
            upgradeButton.interactable = _coins >= LevelUpCost;
        }

        public override void ToggleControls()
        {
            _controlsShown = !_controlsShown;
            if (_level < 4)
            {
                upgradeButton.gameObject.SetActive(_controlsShown);
            }
        }

        public override void Upgrade()
        {
            AudioManager.instance.PlaySFX("Click");
            
            _coins -= LevelUpCost;
            PlayerData.UpdateCoins(_coins);

            _level++;
            levelText.text = "Уровень: " + _level;
            PlayerPrefs.SetInt(TownHallLevel, _level);

            StartCoroutine(ShowMoneySpent());
            
            UpdateValues();
        }

        protected override void UpdateValues()
        {
            switch (_level) 
            {
                case 2:
                    _leadership = 340;
                    LevelUpCost = 37500;
                    break;
                case 3:
                    _leadership = 450;
                    LevelUpCost = 50000;
                    break;
                case 4:
                    _leadership = 700;
                    LevelUpCost = 75000;
                    break;
            }
            
            PlayerData.UpdateLeadership(_leadership);
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