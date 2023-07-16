using UnityEngine;

namespace Upgrades
{
    [CreateAssetMenu(fileName = "PassiveItemProperty", menuName = "PassiveItemProperty", order = 0)]
    public class PassiveItemProperty : ScriptableObject
    {
        public PassiveItemType Type;
        public Sprite Sprite;
        public string Title;
        public string Description;
        public int MaxLevel;
    }
}