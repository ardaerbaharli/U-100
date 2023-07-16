using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UI;
using UnityEditor;
using UnityEngine;
using Upgrades;
using Utils;
using Weapons;
using Range = Upgrades.Range;

namespace Managers
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private UpgradePanel upgradePanel;

        public static UpgradeManager Instance;

        private List<PassiveItemProperty> passiveItemProperties;
        private Dictionary<PassiveItemType, PassiveItem> passiveItemMap;

        private void Awake()
        {
            Instance = this;
            passiveItemProperties = new List<PassiveItemProperty>();
            passiveItemProperties = Resources.LoadAll<PassiveItemProperty>("PassiveItems").ToList();

            passiveItemMap = new Dictionary<PassiveItemType, PassiveItem>
            {
                {PassiveItemType.Armor, new Armor()},
                {PassiveItemType.MaxHealth, new MaxHealth()},
                {PassiveItemType.MoveSpeed, new MoveSpeed()},
                {PassiveItemType.Range, new Range()},
                {PassiveItemType.Regeneration, new Regeneration()},
                {PassiveItemType.IncreaseDamage, new IncreaseDamage()}
            };
        }

        public void ShowUpgradeMenu()
        {
            Time.timeScale = 0;
            var upgrades = new List<Upgrade>();

            var weaponUpgrades = GetWeaponUpgrades();
            upgrades.AddRange(weaponUpgrades);

            var passiveItemUpgrades = GetOtherUpgrades();
            upgrades.AddRange(passiveItemUpgrades);

            // select 3 random upgrades
            if (upgrades.Count > 3)
                upgrades = upgrades.OrderBy(x => Guid.NewGuid()).Take(3).ToList();

            upgradePanel.Show(upgrades);
        }

        private List<Upgrade> GetOtherUpgrades()
        {
            var upgrades = new List<Upgrade>();
            foreach (var property in passiveItemProperties)
            {
                var upgrade = new Upgrade();
                if (PlayerManager.Instance.HasPassiveItem(property.Type))
                {
                    upgrade.OnSelected += () => PlayerManager.Instance.UpgradePassiveItem(property.Type);
                }
                else
                {
                    upgrade.OnSelected += () =>
                    {
                        var passiveItem = CreatePassiveItem(property);
                        PlayerManager.Instance.AddPassiveItem(passiveItem);
                    };
                }

                upgrade.Sprite = property.Sprite;
                upgrade.Title = property.Title;
                upgrade.Description = property.Description;
                upgrade.Level = 1;
                upgrade.MaxLevel = property.MaxLevel;
                upgrades.Add(upgrade);
            }

            return upgrades;
        }

        private PassiveItem CreatePassiveItem(PassiveItemProperty property)
        {
            var type = property.Type;

            // Check if the type exists in the dictionary and return the corresponding instance
            if (!passiveItemMap.TryGetValue(type, out var passiveItem)) return null;
            passiveItem.Type = type;
            return passiveItem;
        }


        public List<Upgrade> GetWeaponUpgrades()
        {
            var upgrades = new List<Upgrade>();
            var allWeapons = WeaponManager.Instance.GetWeaponProperties();
            foreach (var weapon in allWeapons)
            {
                var upgrade = new Upgrade();
                if (PlayerManager.Instance.HasWeapon(weapon, out var w))
                {
                    if (w.Level < w.MaxLevel)
                    {
                        upgrade.OnSelected += () => { PlayerManager.Instance.UpgradeWeapon(weapon); };
                        upgrade.Sprite = weapon.Sprite;
                        upgrade.Title = weapon.Name;
                        upgrade.Level = w.Level + 1;
                        upgrade.Description = weapon.WeaponType switch
                        {
                            WeaponType.TargetBase => weapon.TargetBaseWeaponProperty.GetUpgradeData(w.Level + 1)
                                .UpgradeDescription,
                            WeaponType.Area => weapon.AreaWeaponProperty.GetUpgradeData(w.Level + 1).UpgradeDescription,
                            _ => upgrade.Description
                        };

                        upgrades.Add(upgrade);
                    }
                }
                else
                {
                    if (weapon.WeaponType == WeaponType.Area)
                    {
                        upgrade.OnSelected += () =>
                        {
                            var areaWeapon = WeaponManager.Instance.AddWeapon(PlayerManager.Instance.weaponParent,
                                weapon.AreaWeaponProperty.WeaponType,
                                WeaponTarget.Enemy);
                            PlayerManager.Instance.AddWeapon(areaWeapon);
                        };
                    }
                    else if (weapon.WeaponType == WeaponType.TargetBase)
                    {
                        upgrade.OnSelected += () =>
                        {
                            var targetBaseWeapon = WeaponManager.Instance.AddWeapon(PlayerManager.Instance.weaponParent,
                                weapon.TargetBaseWeaponProperty.WeaponType, true);
                            PlayerManager.Instance.AddWeapon(targetBaseWeapon);
                        };
                    }

                    upgrade.Sprite = weapon.Sprite;
                    upgrade.Title = weapon.Name;
                    upgrade.Description = "New Weapon";
                    upgrade.Level = 1;
                    upgrades.Add(upgrade);
                }
            }

            return upgrades;
        }
    }
}