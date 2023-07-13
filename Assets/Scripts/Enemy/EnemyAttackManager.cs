using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackManager : MonoBehaviour
    {
        public LayerMask detectionLayer;
        private float _cooldownTimer;
        private EnemyController _enemyController;
        private EnemyProperty _enemyProperty;

        private bool _playerDetected;


        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
            _enemyController.OnPropertySet += SetEnemyProperty;
        }

        private void Start()
        {
            _cooldownTimer = _enemyProperty.AttackCooldown;
        }

        private void Update()
        {
            PerformDetection();
            if (_cooldownTimer > 0) _cooldownTimer -= Time.deltaTime;

            if (_playerDetected && _cooldownTimer < 0) Attack();
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _enemyProperty.Range);
        }

        private void SetEnemyProperty(EnemyProperty obj)
        {
            _enemyProperty = obj;
        }

        private void PerformDetection()
        {
            var col = Physics2D.OverlapCircle(transform.position, _enemyProperty.Range, detectionLayer);
            if (col != null)
            {
                _playerDetected = true;
                _enemyController.isFollowingPlayer = false;
            }
            else
            {
                _playerDetected = false;
                _enemyController.isFollowingPlayer = true;
            }
        }

        private void Attack()
        {
            print("Attack");
            _cooldownTimer = _enemyProperty.AttackCooldown;
            _enemyController.isFollowingPlayer = false;
            PlayerManager.Instance.TakeDamage(_enemyProperty.Damage);
        }
    }
}