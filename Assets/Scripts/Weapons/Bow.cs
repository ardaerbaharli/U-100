using Managers;

namespace Weapons
{
    public class Bow : RangedWeapon
    {
        private void Awake()
        {
            AmmoPooledObjectType = PooledObjectType.Arrow;
        }
    }
}