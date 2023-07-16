using Player;

namespace Upgrades
{
    public class Range : PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.IncreaseRange(10);
        }
    }
}