using System;
using System.Collections.Generic;
using Player;
using UI;
using UnityEngine;
using Utils;
using Weapons;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradePanel upgradePanel;

        public static UpgradeManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        /*
3 tane upgrade göster
bunlardan birisi yeni silah olabilir
bu upgradeleri player weaponlarından çek
get weapon levle
get upgrades

         */
        public void ShowUpgradeMenu()
        {
            Time.timeScale = 0;
            var upgrades = GetPossibleUpgrades();
            var remaining = 3 - upgrades.Count;
            var newWeapons = GetNewWeapons(remaining);

            upgradePanel.Show(upgrades, newWeapons);
        }


        private List<Weapon> GetPossibleUpgrades()
        {
            var weapons = new List<Weapon>();
            var currentWeapons = PlayerManager.Instance.GetEquippedWeapons();
            foreach (var weapon in currentWeapons)
            {
                if (weapon.Level < weapon.MaxLevel)
                {
                    weapons.Add(weapon);
                }
            }

            weapons.Shuffle();
            return weapons;
        }

        private List<WeaponProperty> GetNewWeapons(int remaining)
        {
            var equippedWeapons = PlayerManager.Instance.GetEquippedWeapons();
            var allWeapons = WeaponManager.Instance.GetWeaponProperties();

            foreach (var equippedWeapon in equippedWeapons)
            {
                if (equippedWeapon.Type == WeaponType.Area)
                {
                    allWeapons.Remove(allWeapons.Find(x =>x.WeaponType== WeaponType.Area &&
                        x.AreaWeaponProperty.WeaponType == equippedWeapon.AreaWeaponType));
                }
                else if (equippedWeapon.Type == WeaponType.TargetBase)
                {
                    allWeapons.Remove(allWeapons.Find(x =>x.WeaponType== WeaponType.TargetBase &&
                        x.TargetBaseWeaponProperty.WeaponType == equippedWeapon.TargetBaseWeaponType));
                }
            }
            
            allWeapons.Shuffle();
            return allWeapons.GetRange(0, remaining);
            
        }

       
    }
}