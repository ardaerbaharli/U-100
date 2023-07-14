using System;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        private int _level;

        public int Level
        {
            get => _level;
            private set
            {
                _level = value;
                OnLevelUp?.Invoke(_level);
            }
        }

        public Action<int> OnLevelUp;

        public static LevelManager Instance;
        [SerializeField] private float pointsToLevelUp;
        
        private float _previousPointsToLevelUp;
        private float _remainingPointsToLevelUp;
        public Action<float> RemainingPointsChanged;

        private void Awake()
        {
            Instance = this;
            Level = 1;
            
            _remainingPointsToLevelUp = pointsToLevelUp;
            _previousPointsToLevelUp = pointsToLevelUp;
        }

        public void PointsReceived(float points)
        {
            _remainingPointsToLevelUp -= points;
            var remainingPercentage = _remainingPointsToLevelUp / _previousPointsToLevelUp;
            RemainingPointsChanged?.Invoke(remainingPercentage);
            if (_remainingPointsToLevelUp <= 0)
            {
                LevelUp();
            }
        }

        [Button()]
        private void LevelUp()
        {
            Level++;
            _remainingPointsToLevelUp = _previousPointsToLevelUp * 1.5f;
            _previousPointsToLevelUp = _remainingPointsToLevelUp;
            UpgradeManager.Instance.ShowUpgradeMenu();
        }
    }
}