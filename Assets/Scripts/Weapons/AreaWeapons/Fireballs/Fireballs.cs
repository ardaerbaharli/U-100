using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Fireballs : AreaWeapon
    {
        [SerializeField] private List<Ball> fireballs;
        private float speed;


        private IEnumerator StartRotatingAround()
        {
            while (true)
            {
                transform.RotateAround(transform.position, Vector3.forward, speed * Time.deltaTime);
                yield return null;
            }
        }

        public override void Upgraded()
        {
            SetLevelFireballs();
        }

        public override void UpdateDamage(float multiplier)
        {
            for (var i = 0; i < fireballs.Count; i++)
            {
                 fireballs[i].damage = Damage * DamageMultiplier;
            }
        }

        public override void StartAttack()
        {
            speed = Property.AreaWeaponProperty.GetUpgradeData(Level).Speed;
            SetLevelFireballs();

            StartCoroutine(StartRotatingAround());
        }

        private void SetLevelFireballs()
        {
            if (Level > fireballs.Count)
                return;

            for (var i = 0; i < fireballs.Count; i++)
            {
                var fireball = fireballs[i];
                fireball.SetProperties(WeaponTarget, Damage * DamageMultiplier);
                fireball.gameObject.SetActive(true);
            }
        }
    }
}