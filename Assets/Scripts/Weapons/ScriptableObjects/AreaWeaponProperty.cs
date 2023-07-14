using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [Serializable]
    public class AreaWeaponProperty
    {
        public AreaWeaponType WeaponType;
        public GameObject WeaponPrefab;
        public List<AreaWeaponUpgradeData> UpgradeData;
        
        public AreaWeaponUpgradeData GetUpgradeData(int level)
        {
            return UpgradeData[level - 1];
        }
    }
}