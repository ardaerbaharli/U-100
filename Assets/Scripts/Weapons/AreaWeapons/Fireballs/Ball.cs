using UnityEngine;

namespace Weapons
{
    public class Ball : MonoBehaviour
    {
        public WeaponTarget weaponTarget;
        public float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (weaponTarget == WeaponTarget.Player)
            {
                if (!other.CompareTag("Player")) return;
                other.GetComponent<Target>().TakeDamage(damage);
            }
            else if (weaponTarget == WeaponTarget.Enemy)
            {
                if (!other.CompareTag("Enemy")) return;
                other.GetComponent<Target>().TakeDamage(damage);
            }
        }

        public void SetProperties(WeaponTarget weaponTarget1, float damage)
        {
            weaponTarget = weaponTarget1;
            this.damage = damage;
        }
    }
}