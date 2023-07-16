using Managers;
using UnityEngine;

namespace UI
{
    public class GameOverPanel : MonoBehaviour
    {
        public void PlayAgainButton()
        {
            GameManager.Instance.RestartGame();
        }
    }
}