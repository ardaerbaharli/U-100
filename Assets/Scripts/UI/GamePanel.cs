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
        [SerializeField] private Image pauseButtonImage;

        [SerializeField] private Sprite pauseSprite, resumeSprite;

        private bool _isPaused;

        private void Start()
        {
            TimeManager.Instance.OnTimeChanged += UpdateTime;
            LevelManager.Instance.OnLevelUp += UpdateLevel;
            LevelManager.Instance.RemainingPointsChanged += UpdatePointsSlider;
            GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState obj)
        {
            pauseButtonImage.sprite = obj switch
            {
                GameState.Paused => resumeSprite,
                GameState.Game => pauseSprite,
                _ => pauseButtonImage.sprite
            };
        }

        private void UpdatePointsSlider(float obj)
        {
            pointsSlider.value = 1 - obj;
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

        public void PauseButton()
        {
            if (_isPaused)
                GameManager.Instance.ResumeGame();
            else
                GameManager.Instance.PauseGame();
            
            _isPaused = !_isPaused;
        }
    }
}