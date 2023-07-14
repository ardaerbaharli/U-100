using System.Collections.Generic;
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
        private TargetBaseWeapon _firstWeapon;
        private List<Weapon> _weapons;

        private void Awake()
        {
            Instance = this;

            _collider = GetComponent<BoxCollider2D>();
            _weapons = new List<Weapon>();

            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerMovementManager.SetDirection(characterStartDirection);
            playerMovementManager.OnFlip += FlipSprite;
        }

        private void Start()
        {
            _firstWeapon = WeaponManager.Instance.AddWeapon(weaponParent, TargetBaseWeaponType.Wand, true);
            _firstWeapon.WeaponTarget = WeaponTarget.Enemy;
            _firstWeapon.Range += _collider.size.x;
            _weapons.Add(_firstWeapon);

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

        public void Heal(int healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            if (weapon.Type == WeaponType.TargetBase)
            {
                var targetBaseWeapon = (TargetBaseWeapon) weapon;
                targetBaseWeapon.WeaponTarget = WeaponTarget.Enemy;
                targetBaseWeapon.Range += _collider.size.x;
            }

            _weapons.Add(weapon);
        }

        public List<Weapon> GetEquippedWeapons()
        {
            return _weapons;
        }

        public void UpgradeWeapon(WeaponProperty weaponProperty)
        {
            foreach (var weapon in _weapons)
            {
                if (weapon.Type == WeaponType.Area && weaponProperty.WeaponType == WeaponType.Area)
                {
                    if (weapon.AreaWeaponType == weaponProperty.AreaWeaponProperty.WeaponType)
                        weapon.Upgrade();
                }
                else if (weapon.Type == WeaponType.TargetBase && weaponProperty.WeaponType == WeaponType.TargetBase)
                {
                    if (weapon.TargetBaseWeaponType == weaponProperty.TargetBaseWeaponProperty.WeaponType)
                        weapon.Upgrade();
                }
            }
        }
    }
}