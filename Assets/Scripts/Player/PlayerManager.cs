using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Direction characterStartDirection;
        public PlayerMovementManager _playerMovementManager;
        private Weapon _weapon;


        [SerializeField] float _maxHealth;
        private float _currentHealth;

        private void Awake()
        {
            Instance = this;
            _weapon = GetComponentInChildren<Weapon>();
            _playerMovementManager = GetComponent<PlayerMovementManager>();
            _playerMovementManager.SetDirection(characterStartDirection);
            _playerMovementManager.OnFlip += FlipSprite;
        }

        private void Start()
        {
            GameManager.Instance.OnGameStarted += OnGameStarted;
            _currentHealth = _maxHealth;
        }

        private void OnGameStarted()
        {
            _weapon.Attack();
        }
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                //die
                GameManager.Instance.GameOver();
            }
        }

        public void FlipSprite()
        {
            sprite.flipX = !sprite.flipX;
        }
    }
}