using Managers;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerManager : Target
    {
        public static PlayerManager Instance;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Direction characterStartDirection;
        [SerializeField] private GameObject weaponParent;

        public PlayerMovementManager playerMovementManager;
        [SerializeField] private float _maxHealth;
        private float _currentHealth;


        private bool _isDead;
        private Weapon _weapon;

        private void Awake()
        {
            Instance = this;


            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerMovementManager.SetDirection(characterStartDirection);
            playerMovementManager.OnFlip += FlipSprite;
        }

        private void Start()
        {
            _weapon = WeaponManager.Instance.AddWeapon(weaponParent, WeaponType.Wand);
            _weapon.WeaponTarget = WeaponTarget.Enemy;
            GameManager.Instance.OnGameStarted += OnGameStarted;
            _currentHealth = _maxHealth;
        }

        private void OnGameStarted()
        {
            _isDead = false;
        }

        public override void TakeDamage(float damage)
        {
            if (_isDead) return;
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
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