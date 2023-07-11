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

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }

        private void FixedUpdate()
        {
            if (!_enemyController.isFollowingPlayer) return;

            var playerPosition = _player.transform.position;
            var enemyPosition = transform.position;
            var direction = (playerPosition - enemyPosition).normalized;
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

        public void StartFollowing(PlayerManager player, float speed)
        {
            _speed = speed;
            _player = player;
        }
    }
}