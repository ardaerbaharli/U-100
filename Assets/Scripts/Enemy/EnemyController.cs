using System;
using Collectables;
using Managers;
using Player;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyController : Target
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject weaponParent;

        public bool isFollowingPlayer;
        public PooledObject pooledObject;
        public PlayerManager _player;
        public bool isDead;
        private readonly Direction startDirection = Direction.Right;
        private float _currentHealth;
        private EnemyMovementManager _enemyMovementManager;

        private EnemyProperty _enemyProperty;

        private float _maxHealth;
        private float _rangeAdvantage;

        [NonSerialized] public Direction Direction;
        public Action OnEnemyDied;
        public Action<EnemyProperty> OnPropertySet;
        private Animator _animator;
        private bool _isBoss;

        private void Awake()
        {
            _enemyMovementManager = GetComponent<EnemyMovementManager>();
            _animator = GetComponent<Animator>();
            _enemyMovementManager.OnFlip += FlipSprite;
        }


        public void StartFollowing(PlayerManager p)
        {
            _player = p;
            _enemyMovementManager.StartFollowing(_player, _enemyProperty.Speed);
            isFollowingPlayer = true;
            // set the direction of the enemy to the player
            var playerDirection = (_player.transform.position - transform.position).normalized;
            Direction = playerDirection.x > 0 ? Direction.Right : Direction.Left;
            if ((Direction == Direction.Left && startDirection == Direction.Right) ||
                (Direction == Direction.Right && startDirection == Direction.Left))
                FlipSprite();
        }

        private void FlipSprite()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        public void ReturnToPool()
        {
            pooledObject.ReturnToPool();
        }

        public void SetEnemyProperty(EnemyProperty enemyProperty)
        {
            _enemyProperty = enemyProperty;
            spriteRenderer.sprite = enemyProperty.Sprite;
            _maxHealth = enemyProperty.Health;
            _currentHealth = _maxHealth;

            _isBoss = enemyProperty.IsBoss;
            _animator.runtimeAnimatorController = enemyProperty.animator;

            var w = WeaponManager.Instance.AddWeapon(weaponParent, enemyProperty.WeaponType);
            w.Damage *= enemyProperty.DamageMultiplier;
            w.WeaponTarget = WeaponTarget.Player;
            OnPropertySet?.Invoke(_enemyProperty);
        }


        public override void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            LevelManager.Instance.PointsReceived(_maxHealth);
            CollectableManager.Instance.SpawnCollectable(CollectableType.Coin, transform.position);
            if (_isBoss)
                CollectableManager.Instance.SpawnCollectable(CollectableType.Chest, transform.position);
            isDead = true;
            ReturnToPool();
        }
    }
}