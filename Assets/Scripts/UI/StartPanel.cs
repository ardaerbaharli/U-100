using Managers;
using UnityEngine;

namespace UI
{
    public class StartPanel : MonoBehaviour
    {
        public void StartButton()
        {
            GameManager.Instance.StartGame();
        }
    }
}