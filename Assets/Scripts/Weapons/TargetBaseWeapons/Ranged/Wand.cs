using Managers;

namespace Weapons
{
    public class Wand : RangedWeapon
    {
        private void Awake()
        {
            AmmoPooledObjectType = PooledObjectType.WandAmmo;
        }
    }
}