using NaughtyAttributes;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponProperty", menuName = "WeaponProperty", order = 0)]
    public class WeaponProperty : ScriptableObject
    {
        public WeaponType WeaponType;

        [ShowIf("WeaponType", WeaponType.TargetBase)]
        public TargetBaseWeaponProperty TargetBaseWeaponProperty;

        [ShowIf("WeaponType", WeaponType.Area)]
        public AreaWeaponProperty AreaWeaponProperty;
    }
}