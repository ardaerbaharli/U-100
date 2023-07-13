using System;
using Managers;
using Player;
using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyController : Target
    {
        [SerializeField] private SpriteRenderer sprite;
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

        private void Awake()
        {
            _enemyMovementManager = GetComponent<EnemyMovementManager>();
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
            sprite.flipX = !sprite.flipX;
        }

        public void ReturnToPool()
        {
            pooledObject.ReturnToPool();
        }

        public void SetEnemyProperty(EnemyProperty enemyProperty)
        {
            _enemyProperty = enemyProperty;
            sprite.sprite = enemyProperty.Sprite;
            _maxHealth = enemyProperty.Health;
            _currentHealth = _maxHealth;
            // AddWeapon(enemyProperty.WeaponType);

            var w = WeaponManager.Instance.AddWeapon(weaponParent, enemyProperty.WeaponType);
            w.WeaponTarget = WeaponTarget.Player;
            OnPropertySet?.Invoke(_enemyProperty);
        }


        public override void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                isDead = true;
                ReturnToPool();
            }
        }
    }
}