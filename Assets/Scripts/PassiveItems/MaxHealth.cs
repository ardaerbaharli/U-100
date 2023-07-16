using Player;

namespace Upgrades
{
    public class MaxHealth:PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.MaxHealth += PlayerManager.Instance.MaxHealth * 0.1f;
        }
    }
}