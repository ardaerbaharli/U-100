using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private Slider pointsSlider;
        

        private void Start()
        {
            // Removed temporarily
            // ScoreManager.Instance.OnScoreChanged += UpdateScore;
            TimeManager.Instance.OnTimeChanged += UpdateTime;
            LevelManager.Instance.OnLevelUp += UpdateLevel;
            LevelManager.Instance.RemainingPointsChanged+= UpdatePointsSlider;
            
            
        }

        private void UpdatePointsSlider(float obj)
        {
            pointsSlider.value = 1-obj;
        }

        private void UpdateLevel(int obj)
        {
            levelText.text = $"Level {obj}";
        }

        private void UpdateTime(float seconds)
        {
            // convert seconds to minutes and seconds
            var minutes = Mathf.FloorToInt(seconds / 60);
            var remainingSeconds = Mathf.FloorToInt(seconds % 60);
            timeText.text = $"{minutes:00}:{remainingSeconds:00}";
        }

       
    }
}