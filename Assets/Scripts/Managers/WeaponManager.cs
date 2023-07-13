using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;

namespace Managers
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance;
        private List<WeaponProperty> _weaponProperties;

        private void Awake()
        {
            Instance = this;
            _weaponProperties = new List<WeaponProperty>();
            // load weaponproperties from Resources/WeaponProperties
            var weaponProperties = Resources.LoadAll<WeaponProperty>("WeaponProperties");
            _weaponProperties.AddRange(weaponProperties);
        }

        public Weapon AddWeapon(GameObject to, WeaponType type)
        {
            Weapon w;
            switch (type)
            {
                case WeaponType.Bow:
                    w = to.AddComponent<Bow>();
                    break;
                case WeaponType.Wand:
                    w = to.AddComponent<Wand>();
                    break;
                case WeaponType.Melee:
                    w = to.AddComponent<MeleeWeapon>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            w.SetProperties(GetWeaponProperty(type));
            return w;
        }

        private WeaponProperty GetWeaponProperty(WeaponType type)
        {
            return _weaponProperties.FirstOrDefault(weaponProperty => weaponProperty.WeaponType == type);
        }
    }
}