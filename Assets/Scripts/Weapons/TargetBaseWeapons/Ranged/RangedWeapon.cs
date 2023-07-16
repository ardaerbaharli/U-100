using Managers;
using Player;
using UnityEngine;

namespace Weapons
{
    public class RangedWeapon : TargetBaseWeapon
    {
        private PlayerManager _player;
        protected PooledObjectType AmmoPooledObjectType;

        public PlayerManager Player
        {
            get
            {
                if (_player == null) _player = PlayerManager.Instance;
                return _player;
            }
        }


        public override void Attack()
        {
            var projectilePooledObject = ObjectPool.Instance.GetPooledObject(AmmoPooledObjectType);
            var projectile = projectilePooledObject.gameObject.GetComponent<Ammo>();
            projectile.PooledObject = projectilePooledObject;

            projectile.SetAmmoProperty(Damage*DamageMultiplier, Speed, Range, WeaponTarget);
            projectile.transform.position = transform.position;

            Vector2 direction;
            if (WeaponTarget == WeaponTarget.Enemy)
                direction = EnemySpawnManager.Instance.GetClosestEnemyDirection(Player.transform.position);
            else
                direction = (Player.transform.position - transform.position).normalized;


            projectile.gameObject.SetActive(true);
            projectile.Shoot(direction);
        }

      
    }
}