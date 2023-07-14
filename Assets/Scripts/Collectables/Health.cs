using Player;
using UnityEngine;

namespace Collectables
{
    public class Health : Collectable
    {
        public int HealAmount;
        public override void SetSpecificProperty(CollectableProperty property)
        {
            HealAmount = property.HealAmount;
        }

        public override void GetCollected()
        {
            PlayerManager.Instance.Heal(HealAmount);
        }
    }
}