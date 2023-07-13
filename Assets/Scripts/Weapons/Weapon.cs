using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        private WeaponTarget _weaponTarget;
        public WeaponTarget WeaponTarget
        {
            get=> _weaponTarget;
            set
            {
                _weaponTarget = value;
                _detectionLayer = WeaponTarget == WeaponTarget.Enemy
                    ? LayerMask.GetMask("Enemy")
                    : LayerMask.GetMask("Player");

                if (WeaponTarget == WeaponTarget.Player)
                    target = PlayerManager.Instance;
            }
        }
        public bool TargetInRange;
        public Target target;

        private LayerMask _detectionLayer;
        public Action<bool> TargetInRangeChanged;
        public float Damage { get; private set; }
        public float Speed { get; private set; }
        public float Range { get; private set; }
        public float AttackInterval { get; private set; }


        public void Start()
        {
            StartCoroutine(TargetInRangeCoroutine());
            StartCoroutine(AttackCoroutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(AttackInterval);
                if (TargetInRange) Attack();
            }
        }

        private IEnumerator TargetInRangeCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                // check if target is in range 
                var col = Physics2D.OverlapCircle(transform.position, Range, _detectionLayer);
                if (col != null)
                {
                    if (WeaponTarget == WeaponTarget.Enemy)
                        target = col.GetComponent<Target>();
                    TargetInRange = true;
                    TargetInRangeChanged?.Invoke(true);
                }
                else
                {
                    TargetInRange = false;
                    TargetInRangeChanged?.Invoke(false);
                }
            }
        }

        public abstract void Attack();

        public void SetProperties(WeaponProperty getWeaponProperty)
        {
            Damage = getWeaponProperty.Damage;
            Speed = getWeaponProperty.Speed;
            Range = getWeaponProperty.Range;
            AttackInterval = getWeaponProperty.AttackInterval;
            
           
        }
    }
}