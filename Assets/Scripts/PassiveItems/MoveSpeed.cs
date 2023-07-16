using Player;

namespace Upgrades
{
    public class MoveSpeed : PassiveItem
    {
        public override void Apply()
        {
            PlayerManager.Instance.playerMovementManager.IncreaseSpeed(20);
        }
    }
}