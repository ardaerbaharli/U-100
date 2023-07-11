using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public float Damage;
        public float Speed;
        public float Range;
        public float AttackInterval;

        public abstract void Attack();
    }
}