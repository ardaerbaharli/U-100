using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponProperty", menuName = "WeaponProperty", order = 0)]
    public class WeaponProperty : ScriptableObject
    {
        public WeaponType WeaponType;
        public float Damage;
        public float Speed;
        public float Range;
        public float AttackInterval;
    }
}