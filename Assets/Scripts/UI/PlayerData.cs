using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerData : MonoBehaviour
    {
        private const string CoinsData = "Coins";
        private const string LeadershipData = "Leadership";
        
        [SerializeField] private TMP_Text coinsText; 
        [SerializeField] private TMP_Text leadershipText; 
        
        public int Coins { get; private set; }
        public int Leadership { get; private set; }
        
        private void Start()
        {
            Coins = PlayerPrefs.GetInt(CoinsData);
            Leadership = PlayerPrefs.GetInt(LeadershipData);
            UpdateText();
        }

        public void SaveData()
        {
            Coins += 10;
            Leadership += 10;
            UpdateText();
            PlayerPrefs.SetInt(CoinsData, Coins);
            PlayerPrefs.SetInt(LeadershipData, Leadership);
        }

        private void UpdateText()
        {
            coinsText.text = Coins.ToString();
            leadershipText.text = Leadership.ToString();
        }
    }
}
