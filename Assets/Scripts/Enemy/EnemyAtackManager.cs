using System;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAtackManager : MonoBehaviour
    {
        private EnemyController _enemyController;
        private PlayerManager _player;
        [SerializeField] float _range;
        [SerializeField] float _cooldown;
        private float cdTimer;

        bool playerDetected;
        public LayerMask detectionLayer;
         

        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();
        }
        private void Start()
        {
            cdTimer = _cooldown;
        }
        private void Update()
        {
            performDetection();
            if (cdTimer > 0) { cdTimer -= Time.deltaTime; }
           
            if (playerDetected && cdTimer<0) {
                Atack();
                
            }
               
            
        }
        public void performDetection(){

            Collider2D collider =Physics2D.OverlapCircle(transform.position, _range, detectionLayer);
            if(collider != null) { playerDetected = true;
                _enemyController.isFollowingPlayer = false;

            }
            else { playerDetected = false;
                _enemyController.isFollowingPlayer = true;
            }
        }
        private void Atack()
        {
            cdTimer = _cooldown;
            _enemyController.isFollowingPlayer = false;
            Debug.Log("atacked");
        }
        public void StartFollowing(PlayerManager player, float speed)
        {
            
            _player = player;
        }
       
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
        }
    }
}