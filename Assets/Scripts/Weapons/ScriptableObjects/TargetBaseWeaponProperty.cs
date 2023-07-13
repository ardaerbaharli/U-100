using System;

namespace Weapons
{
    [Serializable]
    public class TargetBaseWeaponProperty
    {
        public float Damage;
        public float Speed;
        public float Range;
        public float AttackInterval;
        public TargetBaseWeaponType WeaponType;
    }
}