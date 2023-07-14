using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Fireballs : AreaWeapon
    {
        [SerializeField] private List<Ball> fireballs;
        [SerializeField] private float speed;


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

        public override void StartAttack()
        {
            SetLevelFireballs();

            StartCoroutine(StartRotatingAround());
        }

        private void SetLevelFireballs()
        {
            if (Level > fireballs.Count)
                return;
            
            for (var i = 0; i < Level; i++)
            {
                var fireball = fireballs[i];
                fireball.SetProperties(WeaponTarget, Damage);
                fireball.gameObject.SetActive(true);
            }
        }
    }
}