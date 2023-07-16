using Player;

namespace Upgrades
{
    public class IncreaseDamage : PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.IncreaseRange(10);
        }
    }
}