using Player;

namespace Upgrades
{
    public class Regeneration : PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.regenerationAmount += 0.2f;
        }
    }
}