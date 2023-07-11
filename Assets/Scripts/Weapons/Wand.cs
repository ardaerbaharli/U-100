using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Managers;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Weapons
{
    public class Wand : Weapon
    {
        public AmmoType ammoType;

        private PooledObjectType _ammoPooledObjectType;
        private PlayerManager _player;

        private void Awake()
        {
            _ammoPooledObjectType =
                (PooledObjectType) Enum.Parse(typeof(PooledObjectType), ammoType + "Bullet");
        }

        private void Start()
        {
            _player = PlayerManager.Instance;
        }

        public override void Attack()
        {
            StartCoroutine(AttackCoroutine());
        }

        private IEnumerator AttackCoroutine()
        {
            // Shoot a projectile in the Player's current direction every AttackInterval seconds
            while (true)
            {
                yield return new WaitForSeconds(AttackInterval);
                var projectilePooledObject = ObjectPool.Instance.GetPooledObject(_ammoPooledObjectType);
                var projectile = projectilePooledObject.gameObject.GetComponent<Ammo>();
                projectile.PooledObject = projectilePooledObject;

                projectile.SetAmmoProperty(Damage, Speed, Range);
                projectile.transform.position = transform.position;

                var direction = EnemySpawnManager.Instance.GetClosestEnemyDirection(_player.transform.position);
                projectile.gameObject.SetActive(true);
                projectile.Shoot(direction);
            }
        }
    }
}