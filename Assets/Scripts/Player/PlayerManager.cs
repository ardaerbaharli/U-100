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
        [SerializeField] public GameObject weaponParent;

        public PlayerMovementManager playerMovementManager;
        [SerializeField] private float _maxHealth;
        private BoxCollider2D _collider;
        private float _currentHealth;


        private bool _isDead;
        private TargetBaseWeapon _weapon;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            Instance = this;


            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerMovementManager.SetDirection(characterStartDirection);
            playerMovementManager.OnFlip += FlipSprite;
        }

        private void Start()
        {
            _weapon = WeaponManager.Instance.AddWeapon(weaponParent, TargetBaseWeaponType.Wand, true);
            _weapon.WeaponTarget = WeaponTarget.Enemy;
            _weapon.Range += _collider.size.x;
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
            if (_currentHealth <= 0)
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