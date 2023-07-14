using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private List<UpgradeUI> upgradeUIs;

        private void Awake()
        {
            upgradeUIs.ForEach(x => x.OnClicked += Hide);
        }

        private void Hide()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void Show(List<Weapon> upgrades, List<WeaponProperty> weaponProperties)
        {
            int i;
            for (i = 0; i < upgrades.Count; i++)
            {
                upgradeUIs[i].SetData(upgrades[i].Property,true, upgrades[i].Level+1);
            }

            foreach (var property in weaponProperties)
            {
                upgradeUIs[i].SetData(property, false, 1);
                i++;
            }

            // hide remaining
            for (var j = i; j < upgradeUIs.Count; j++)
            {
                upgradeUIs[j].Hide();
            }

            gameObject.SetActive(true);
        }
    }
}