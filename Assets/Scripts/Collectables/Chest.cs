using Managers;
using UnityEngine;

namespace Collectables
{
    public class Chest : Collectable
    {
        public override void SetSpecificProperty(CollectableProperty property)
        {
            
        }

        public override void GetCollected()
        {
            UpgradeManager.Instance.ShowUpgradeMenu();
        }
    }
}