using System;
using UnityEngine;

namespace Managers
{
    public class MoneyManager : MonoBehaviour
    {
        private float _money;

        public float Money
        {
            get => _money;
            set
            {
                _money = value;
                OnMoneyChanged?.Invoke(_money);
            }
        }

        public Action<float> OnMoneyChanged;

        public static MoneyManager Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}