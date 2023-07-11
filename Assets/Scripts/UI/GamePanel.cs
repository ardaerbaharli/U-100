using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timeText;

        private void Start()
        {
            // Removed temporarily
            // ScoreManager.Instance.OnScoreChanged += UpdateScore;
            // TimeManager.Instance.OnTimeChanged += UpdateTime;
        }

        private void UpdateTime(float seconds)
        {
            // convert seconds to minutes and seconds
            var minutes = Mathf.FloorToInt(seconds / 60);
            var remainingSeconds = Mathf.FloorToInt(seconds % 60);
            timeText.text = $"{minutes:00}:{remainingSeconds:00}";
        }

        private void UpdateScore(float s)
        {
            scoreText.text = s.ToString("0");
        }
    }
}