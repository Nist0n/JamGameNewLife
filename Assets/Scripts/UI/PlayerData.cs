using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerData : MonoBehaviour
    {
        public const string CoinsData = "Coins";
        public const string LeadershipData = "Leadership";
        
        [SerializeField] private TMP_Text coinsText; 
        [SerializeField] private TMP_Text leadershipText; 
        
        public static int Coins { get; private set; }
        public int Leadership { get; private set; }
        
        private void Start()
        {
            Coins = PlayerPrefs.GetInt(CoinsData);
            Leadership = PlayerPrefs.GetInt(LeadershipData);
        }

        private void Update()
        {
            coinsText.text = PlayerPrefs.GetInt(CoinsData).ToString();
            leadershipText.text = PlayerPrefs.GetInt(LeadershipData).ToString();
        }

        public static void UpdateCoins(int coins)
        {
            PlayerPrefs.SetInt(CoinsData, coins);
        }
        
        public static void UpdateLeadership(int leadership)
        {
            PlayerPrefs.SetInt(LeadershipData, PlayerPrefs.GetInt(LeadershipData) + leadership);
        }
    }
}
