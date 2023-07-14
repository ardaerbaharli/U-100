using UnityEngine;

namespace Collectables
{
    [CreateAssetMenu(fileName = "CollectableProperty", menuName = "CollectableProperty", order = 0)]
    public class CollectableProperty : ScriptableObject
    {
        public CollectableType Type;
        public Sprite Sprite;
        public Vector2 ColliderSize;
        [Header("Specific Properties")] public float CoinValue;
        public int HealAmount;
        
    }
}