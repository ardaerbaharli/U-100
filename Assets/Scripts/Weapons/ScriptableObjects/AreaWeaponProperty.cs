using System;
using UnityEngine;

namespace Weapons
{
    [Serializable]
    public class AreaWeaponProperty
    {
        public AreaWeaponType WeaponType;
        public float Damage;
        public GameObject WeaponPrefab;
    }
}