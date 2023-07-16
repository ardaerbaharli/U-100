using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovementManager : MonoBehaviour
    {
        private EnemyController _enemyController;
        private PlayerManager _player;
        private float _speed;

        public Action OnFlip;

        private BoxCollider2D _boxCollider2D;
        private Vector2 _colliderSize;

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            _colliderSize = _boxCollider2D.size / 4;
        }

        private void FixedUpdate()
        {
            if (!_enemyController.isFollowingPlayer) return;

            var playerPosition = _player.transform.position;
            var enemyPosition = transform.position;
            var direction = (playerPosition - enemyPosition).normalized;
            var projectedPosition = transform.position + direction * (_speed * Time.deltaTime) + GetSpacing(direction);
            // send a raycast on layer Wall at the projected position
            var hit = Physics2D.Raycast(projectedPosition, Vector2.zero, 0, LayerMask.GetMask("Enemy"));
            if (hit.collider != null)
            {
                // if the enemy is going to collide with another enemy, set the movement to 0
                if (!IsMe(hit.collider.gameObject))
                    return;
            }

            transform.Translate(direction * (_speed * Time.deltaTime));

            switch (direction.x)
            {
                case > 0 when _enemyController.Direction == Direction.Left:
                    _enemyController.Direction = Direction.Right;
                    OnFlip?.Invoke();
                    break;
                case < 0 when _enemyController.Direction == Direction.Right:
                    _enemyController.Direction = Direction.Left;
                    OnFlip?.Invoke();
                    break;
            }
        }

        private Vector3 GetSpacing(Vector3 direction)
        {
            // if direction is up or down use collider.Y 
            // if direction is left or right use collider.X
            
            var spacing = Vector3.zero;
            if (direction.x > 0)
            {
                spacing.x = _colliderSize.x;
            }
            else if (direction.x < 0)
            {
                spacing.x = -_colliderSize.x;
            }
            else if (direction.y > 0)
            {
                spacing.y = _colliderSize.y;
            }
            else if (direction.y < 0)
            {
                spacing.y = -_colliderSize.y;
            }
            
            return spacing;
        }

        private bool IsMe(GameObject colliderGameObject)
        {
            return colliderGameObject.name == gameObject.name;
        }

        public void StartFollowing(PlayerManager player, float speed)
        {
            _speed = speed;
            _player = player;
        }
    }
}