using System;
using System.Collections.Generic;

namespace Weapons
{
    [Serializable]
    public class TargetBaseWeaponProperty
    {
        public TargetBaseWeaponType WeaponType;
        public List<TargetBaseWeaponUpgradeData> UpgradeData;
        
        public TargetBaseWeaponUpgradeData GetUpgradeData(int level)
        {
            return UpgradeData[level - 1];
        }
    }
}