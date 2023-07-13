using System;
using Managers;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;

        public bool isFollowingPlayer;
        public PooledObject pooledObject;
        private EnemyMovementManager _enemyMovementManager;

        private EnemyProperty _enemyProperty;
        public PlayerManager _player;

        [NonSerialized] public Direction Direction;
        private readonly Direction startDirection = Direction.Right;
        public Action OnEnemyDied;


        private float _maxHealth;
        private float _currentHealth;
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
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                //die
                transform.position = new Vector3(1000,0,0);
                ReturnToPool();
            }
        }
    }
}