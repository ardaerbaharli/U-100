using System;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public enum GameState
    {
        MainMenu,
        Game,
        Paused,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private float totalGameTime;
        private GameState _gameState;

        public Action OnGameStarted;
        public Action<GameState> OnGameStateChanged;

        public GameState GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                OnGameStateChanged?.Invoke(_gameState);
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            TimeManager.Instance.OnTimeEnded += GameOver;
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            GameState = GameState.GameOver;
            // TODO
        }
        
        [Button()]
        public void StartGame()
        {
            Debug.Log("Game Started");
            GameState = GameState.Game;
            TimeManager.Instance.StartTimer(totalGameTime);
            OnGameStarted?.Invoke();
            // TODO
        }

        public void PauseGame()
        {
            Debug.Log("Game Paused");
            GameState = GameState.Paused;
            // TODO
        }

        public void Scored(float score)
        {
            Debug.Log("Scored");
            // TODO
        }

        public void RestartGame()
        {
            Debug.Log("Restart Game");
            // TODO
        }
    }
}