using System.Collections;
using Managers;
using UnityEngine;

namespace Weapons
{
    public class Ammo : MonoBehaviour
    {
        public PooledObject PooledObject;
        private float _damage;
        private float _range;
        private float _speed;
        private WeaponTarget _weaponTarget;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_weaponTarget == WeaponTarget.Player)
            {
                if (!other.CompareTag("Player")) return;
                other.GetComponent<Target>().TakeDamage(_damage);
                ReturnToPool();
            }
            else if (_weaponTarget == WeaponTarget.Enemy)
            {
                if (!other.CompareTag("Enemy")) return;
                other.GetComponent<Target>().TakeDamage(_damage);
                ReturnToPool();
            }
        }

        public void SetAmmoProperty(float damage, float speed, float range, WeaponTarget weaponTarget)
        {
            _damage = damage;
            _speed = speed;
            _range = range;
            _weaponTarget = weaponTarget;
        }

        public void ReturnToPool()
        {
            StopAllCoroutines();
            PooledObject.ReturnToPool();
        }

        public void Shoot(Vector2 direction)
        {
            // set rotation to face the direction
            // var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            StartCoroutine(ShootCoroutine(direction));
        }

        private IEnumerator ShootCoroutine(Vector2 direction)
        {
            var distance = 0f;
            while (distance < _range)
            {
                distance += _speed * Time.deltaTime;
                transform.Translate(direction * (_speed * Time.deltaTime));
                yield return null;
            }

            ReturnToPool();
        }
    }
}