using Player;

namespace Upgrades
{
    public class Armor:PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.armorAmount += 1;
        }
    }
}