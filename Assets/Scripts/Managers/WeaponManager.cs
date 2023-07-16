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


        public TargetBaseWeapon AddWeapon(GameObject to, TargetBaseWeaponType type, bool asChild = false)
        {
            Type t;
            switch (type)
            {
                case TargetBaseWeaponType.Bow:
                    t = typeof(Bow);
                    break;
                case TargetBaseWeaponType.Wand:
                    t = typeof(Wand);
                    break;
                case TargetBaseWeaponType.Melee:
                    t = typeof(MeleeWeapon);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            TargetBaseWeapon w;
            if (asChild)
            {
                var child = new GameObject(type.ToString());
                child.transform.SetParent(to.transform,false);
                child.transform.localPosition = Vector3.zero;
                w = (TargetBaseWeapon) child.AddComponent(t);
            }
            else
            {
                w = (TargetBaseWeapon) to.AddComponent(t);
            }


            w.SetWeapon(GetWeaponProperty(type));
            return w;
        }

        public AreaWeapon AddWeapon(GameObject to, AreaWeaponType type, WeaponTarget weaponTarget)
        {
            var level = 1;
            var properties = GetWeaponProperty(type);
            var weapon = Instantiate(properties.AreaWeaponProperty.WeaponPrefab, to.transform,false);
            weapon.transform.localPosition = Vector3.zero;

            var a = weapon.GetComponent<AreaWeapon>();
            a.SetWeapon(properties, weaponTarget);
            return a;
        }


        private WeaponProperty GetWeaponProperty(AreaWeaponType type)
        {
            return _weaponProperties.Where(x => x.WeaponType == WeaponType.Area).FirstOrDefault(weaponProperty =>
                weaponProperty.AreaWeaponProperty.WeaponType == type);
        }

        private WeaponProperty GetWeaponProperty(TargetBaseWeaponType type)
        {
            return _weaponProperties.Where(x => x.WeaponType == WeaponType.TargetBase).FirstOrDefault(weaponProperty =>
                weaponProperty.TargetBaseWeaponProperty.WeaponType == type);
        }

        public List<WeaponProperty> GetWeaponProperties()
        {
            return _weaponProperties;
        }
    }
}