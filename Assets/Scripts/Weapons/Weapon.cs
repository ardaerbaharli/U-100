using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public Sprite Sprite;
        public string Name;
        public string UpgradeDescription;
        public WeaponType Type;
        public AreaWeaponType AreaWeaponType;
        public TargetBaseWeaponType TargetBaseWeaponType;
        public int Level;
        public int MaxLevel;

        public WeaponProperty Property;
        public abstract void Upgrade();


        
    }
}