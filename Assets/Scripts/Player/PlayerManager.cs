using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Direction characterStartDirection;
        private PlayerMovementManager _playerMovementManager;


        private void Awake()
        {
            Instance = this;

            _playerMovementManager = GetComponent<PlayerMovementManager>();
            _playerMovementManager.SetDirection(characterStartDirection);
            _playerMovementManager.OnFlip += FlipSprite;
        }

        public void FlipSprite()
        {
            sprite.flipX = !sprite.flipX;
        }
    }
}