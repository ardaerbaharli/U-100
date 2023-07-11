using Enemy.EnemyTypes;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyProperty", menuName = "EnemyProperty", order = 0)]
    public class EnemyProperty : ScriptableObject
    {
        public Sprite Sprite;
        public EnemyType Type;
        public bool IsBoss;
        public float Health;
        public float Damage;
        public float Speed;
    }
}