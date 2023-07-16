using Enemy.EnemyTypes;
using UnityEngine;
using Weapons;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyProperty", menuName = "EnemyProperty", order = 0)]
    public class EnemyProperty : ScriptableObject
    {
        public Sprite Sprite;
        public Sprite OnHitSprite;
        public EnemyType Type;
        public bool IsBoss;
        public float Health;
        public float Speed;
        public TargetBaseWeaponType WeaponType;
        public RuntimeAnimatorController animator;
        public float DamageMultiplier = 1;
    }
}