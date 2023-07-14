using Managers;
using UnityEngine;

namespace Collectables
{
    public class Coin : Collectable
    {
        public float CoinValue;
        public override void SetSpecificProperty(CollectableProperty property)
        {
            CoinValue = property.CoinValue;
        }

        public override void GetCollected()
        {
            MoneyManager.Instance.Money += CoinValue;
            LevelManager.Instance.PointsReceived(CoinValue);
            ReturnToPool();
        }
    }
}