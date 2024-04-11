using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerData : MonoBehaviour
    {
        public const string CoinsData = "Coins";
        public const string LeadershipData = "Leadership";
        
        [SerializeField] private TMP_Text _coinsText; 
        [SerializeField] private TMP_Text _leadershipText; 
        
        public static int Coins { get; private set; }
        public int Leadership { get; private set; }
        
        private void Start()
        {
            Coins = PlayerPrefs.GetInt(CoinsData);
            Leadership = PlayerPrefs.GetInt(LeadershipData);
        }

        private void Update()
        {
            _coinsText.text = PlayerPrefs.GetInt(CoinsData).ToString();
            _leadershipText.text = PlayerPrefs.GetInt(LeadershipData).ToString();
        }
        
        public void SaveData()
        {
            AddCoins(10);
            AddLeadership(10);
        }

        public static void AddCoins(int coins)
        {
            PlayerPrefs.SetInt(CoinsData, coins);
        }
        
        public static void AddLeadership(int leadership)
        {
            PlayerPrefs.SetInt(LeadershipData, leadership);
        }
    }
}
