using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;
using Weapons;

namespace Player
{
    public class PlayerManager : Target
    {
        public static PlayerManager Instance;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Direction characterStartDirection;
        [SerializeField] public GameObject weaponParent;
        [SerializeField] private Slider healthBar;

        public PlayerMovementManager playerMovementManager;
        [SerializeField] private float _maxHealth;

        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                UpdateHealthBar();
            }
        }

        private BoxCollider2D _collider;

        private List<PassiveItem> _passiveItems;


        private float _currentHealth;

        private float CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = value;
                UpdateHealthBar();
            }
        }

        private bool _isDead;
        private TargetBaseWeapon _firstWeapon;
        private List<Weapon> _weapons;

        private void Awake()
        {
            Instance = this;
            _passiveItems = new List<PassiveItem>();

            _collider = GetComponent<BoxCollider2D>();
            _weapons = new List<Weapon>();

            playerMovementManager = GetComponent<PlayerMovementManager>();
            playerMovementManager.SetDirection(characterStartDirection);
            playerMovementManager.OnFlip += FlipSprite;

            StartCoroutine(RegenerationCoroutine());
        }

        public float regenerationAmount;

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (CurrentHealth < MaxHealth)
                {
                    CurrentHealth += regenerationAmount;
                }
            }
        }

        private void Start()
        {
            _firstWeapon = WeaponManager.Instance.AddWeapon(weaponParent, TargetBaseWeaponType.Wand, true);
            AddWeapon(_firstWeapon);

            GameManager.Instance.OnGameStarted += OnGameStarted;
            CurrentHealth = MaxHealth;
        }

        [SerializeField] private float attackSpeedMultiplier;

        public void AddWeapon(Weapon weapon)
        {
            if (weapon.Type == WeaponType.TargetBase)
            {
                var targetBaseWeapon = (TargetBaseWeapon) weapon;
                targetBaseWeapon.WeaponTarget = WeaponTarget.Enemy;
                targetBaseWeapon.AttackInterval /= attackSpeedMultiplier;
                targetBaseWeapon.Range += _collider.size.x;
                weapon.SetDamageMultiplier ( _damageMultiplier / 100);
            }

            _weapons.Add(weapon);
        }

        private void UpdateHealthBar()
        {
            healthBar.value = CurrentHealth / MaxHealth;
        }

        private void OnGameStarted()
        {
            _isDead = false;
        }

        public int armorAmountPercentage;

        public override void TakeDamage(float damage)
        {
            if (_isDead) return;
            damage -= damage * (armorAmountPercentage / 100f);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
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
            CurrentHealth += healAmount;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
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
                    if (weapon.AreaWeaponType != weaponProperty.AreaWeaponProperty.WeaponType) continue;
                    weapon.Upgrade();
                    break;
                }

                if (weapon.Type == WeaponType.TargetBase && weaponProperty.WeaponType == WeaponType.TargetBase)
                {
                    if (weapon.TargetBaseWeaponType != weaponProperty.TargetBaseWeaponProperty.WeaponType) continue;
                    weapon.Upgrade();
                    ((TargetBaseWeapon) weapon).AttackInterval /= attackSpeedMultiplier;
                    break;
                }
            }
        }

        public bool HasWeapon(WeaponProperty property, out Weapon w)
        {
            foreach (var weapon in _weapons)
            {
                if (weapon.Type == property.WeaponType && weapon.Type == WeaponType.Area)
                {
                    if (weapon.AreaWeaponType == property.AreaWeaponProperty.WeaponType)
                    {
                        w = weapon;
                        return true;
                    }
                }
                else if (weapon.Type == property.WeaponType && weapon.Type == WeaponType.TargetBase)
                {
                    if (weapon.TargetBaseWeaponType == property.TargetBaseWeaponProperty.WeaponType)
                    {
                        w = weapon;
                        return true;
                    }
                }
            }

            w = null;
            return false;
        }

        public bool HasPassiveItem(PassiveItemType type)
        {
            return _passiveItems.Any(item => item.Type == type);
        }

        public void AddPassiveItem(PassiveItem passiveItem)
        {
            _passiveItems.Add(passiveItem);
            passiveItem.Apply();
        }

        public void UpgradePassiveItem(PassiveItemType type)
        {
            foreach (var passiveItem in _passiveItems)
            {
                if (passiveItem.Type == type)
                {
                    passiveItem.Apply();
                    break;
                }
            }
        }

        public void IncreaseRange(float percentage)
        {
            foreach (var targetBaseWeapon in _weapons.Where(weapon => weapon.Type == WeaponType.TargetBase)
                         .Cast<TargetBaseWeapon>())
            {
                targetBaseWeapon.Range += targetBaseWeapon.Range * (percentage / 100f);
            }
        }

        private float _damageMultiplier = 100;

        public void IncreaseDamage(float percentage)
        {
            _damageMultiplier += percentage;
            foreach (var weapon in _weapons)
            {
                weapon.SetDamageMultiplier ( _damageMultiplier / 100);
                // if (weapon.Type == WeaponType.Area)
                // {
                //     var w = (AreaWeapon) weapon;
                //     w.Damage += w.Damage * (_damageMultiplier / 100f);
                // }
                // else if (weapon.Type == WeaponType.TargetBase)
                // {
                //     var w = (TargetBaseWeapon) weapon;
                //     w.Damage += w.Damage * (_damageMultiplier / 100f);
                // }
            }
        }
    }
}