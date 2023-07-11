using System;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        private float _score;
        public Action<float> OnScoreChanged;

        public float Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}