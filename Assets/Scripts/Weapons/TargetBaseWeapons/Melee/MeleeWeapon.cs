using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : TargetBaseWeapon
    {
        private float _cooldownTimer;

        private void Update()
        {
            if (_cooldownTimer > 0) _cooldownTimer -= Time.deltaTime;
            else if (TargetInRange) Hit();
        }

        private void Hit()
        {
            print("Attack");
            _cooldownTimer = AttackInterval;
            target.TakeDamage(Damage);
        }

        public override void Attack()
        {
        }
    }
}