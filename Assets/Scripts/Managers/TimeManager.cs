using System;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;
        private float _remainingTime;
        public Action<float> OnTimeChanged;
        public Action OnTimeEnded;

        private void Awake()
        {
            Instance = this;
        }

        public void StartTimer(float totalTime)
        {
            Debug.Log("Start Timer");
            StartCoroutine(StartTimerCoroutine(totalTime));
        }

        private IEnumerator StartTimerCoroutine(float totalTime)
        {
            _remainingTime = totalTime;
            while (_remainingTime > 0)
            {
                _remainingTime -= Time.deltaTime;
                OnTimeChanged?.Invoke(_remainingTime);
                yield return null;
            }

            OnTimeEnded?.Invoke();
        }
    }
}