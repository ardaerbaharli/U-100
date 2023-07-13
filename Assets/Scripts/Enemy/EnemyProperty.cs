using Enemy.EnemyTypes;
using UnityEngine;
using Weapons;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyProperty", menuName = "EnemyProperty", order = 0)]
    public class EnemyProperty : ScriptableObject
    {
        public Sprite Sprite;
        public EnemyType Type;
        public bool IsBoss;
        public float Health;
        public float Speed;
        public float StopDistance;
        public WeaponType WeaponType;
    }
}