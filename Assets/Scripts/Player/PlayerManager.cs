using Managers;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Direction characterStartDirection;
        public PlayerMovementManager playerMovementManager;
        [SerializeField] private float _maxHealth;
        private float _currentHealth;


        private bool _isDead;
        private Weapon _weapon;

        private void Awake()
        {
            Instance = this;
            _weapon = GetComponentInChildren<Weapon>();
            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerMovementManager.SetDirection(characterStartDirection);
            playerMovementManager.OnFlip += FlipSprite;
        }

        private void Start()
        {
            GameManager.Instance.OnGameStarted += OnGameStarted;
            _currentHealth = _maxHealth;
        }

        private void OnGameStarted()
        {
            _isDead = false;
            _weapon.Attack();
        }

        public void TakeDamage(float damage)
        {
            if (_isDead) return;
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                //die
                _isDead = true;
                GameManager.Instance.GameOver();
            }
        }

        public void FlipSprite()
        {
            sprite.flipX = !sprite.flipX;
        }
    }
}