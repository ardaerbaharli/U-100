using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponProperty", menuName = "WeaponProperty", order = 0)]
    public class WeaponProperty : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
        public WeaponType WeaponType;
        public int MaxLevel;

        [ShowIf("WeaponType", WeaponType.TargetBase)]
        public TargetBaseWeaponProperty TargetBaseWeaponProperty;

        [ShowIf("WeaponType", WeaponType.Area)]
        public AreaWeaponProperty AreaWeaponProperty;

       
        
       
    }
}