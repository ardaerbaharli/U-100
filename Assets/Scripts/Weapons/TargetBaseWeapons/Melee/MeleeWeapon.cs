using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : TargetBaseWeapon
    {
        public override void Attack()
        {
            StartCoroutine(StartAttacking());
        }

        private IEnumerator StartAttacking()
        {
            while (true)
            {
                yield return new WaitForSeconds(AttackInterval);
                if (TargetInRange)
                    target.TakeDamage(Damage);
            }
        }
    }
}