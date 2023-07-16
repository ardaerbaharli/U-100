using System;
using System.Collections.Generic;
using UnityEngine;
using Upgrades;
using Weapons;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private List<UpgradeUI> upgradeUIs;

        private void Awake()
        {
            upgradeUIs.ForEach(x => x.OnClicked += ClickedOnUpgrade);
        }

        private void ClickedOnUpgrade(Upgrade upgrade)
        {
            upgrade.OnSelected?.Invoke();
            Hide();
        }

        private void Hide()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

     

        public void Show(List<Upgrade> upgrades)
        {
            var i = 0;
            foreach (var upgrade in upgrades)
            {
                var ui = upgradeUIs[i];
                ui.SetData(upgrade);
                i++;
            }
            
            gameObject.SetActive(true);
        }
    }
}