using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Weapons
{
    public abstract class TargetBaseWeapon : Weapon
    {
        public bool TargetInRange;
        public Target target;

        private LayerMask _detectionLayer;
        private WeaponTarget _weaponTarget;
        public Action<bool> TargetInRangeChanged;

        public WeaponTarget WeaponTarget
        {
            get => _weaponTarget;
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

        public float Damage { get; set; }
        public float Speed { get; private set; }
        public float Range { get; set; }
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
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

        public void SetWeapon(WeaponProperty property)
        {
            Property = property;
            if (property == null)
            {
                print("property is null");
            }

            if (Sprite == null)
                print("sprite is null");

            if (property.Sprite == null)
            {
                print("property sprite is null");
            }
            
            Sprite = property.Sprite;
            Name = property.Name;
            
            Type = WeaponType.TargetBase;
            TargetBaseWeaponType = property.TargetBaseWeaponProperty.WeaponType;

            Level = 1;
            MaxLevel = Property.MaxLevel;

            var data = Property.TargetBaseWeaponProperty.GetUpgradeData(Level);
            SetData(data);
        }

        private void SetData(TargetBaseWeaponUpgradeData data)
        {
            Damage = data.Damage;
            Speed = data.Speed;
            Range = data.Range;
            AttackInterval = data.AttackInterval;
            UpgradeDescription = data.UpgradeDescription;

        }

        public override void Upgrade()
        {
            Level++;
            var upgradeData = Property.TargetBaseWeaponProperty.GetUpgradeData(Level);
            SetData(upgradeData);
        }
    }
}