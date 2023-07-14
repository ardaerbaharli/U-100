using System;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI levelText;

        private bool _isUpgrade;
        private Weapon _upgrade;
        private WeaponProperty _weaponProperty;
        public Action OnClicked;

        private void SetUpgradeUI(Sprite sprite, string title, string description, int level)
        {
            image.sprite = sprite;
            titleText.text = title;
            descriptionText.text = description;
            levelText.text = $"Level {level}";
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

      

        public void SetData(WeaponProperty property, bool isUpgrade, int level)
        {
            _isUpgrade = isUpgrade;
            _weaponProperty = property;
            string desc;
            if (isUpgrade)
            {
                if (property.WeaponType == WeaponType.Area)
                    desc = property.AreaWeaponProperty.GetUpgradeData(level).UpgradeDescription;
                else
                    desc = property.TargetBaseWeaponProperty.GetUpgradeData(level).UpgradeDescription;
            }
            else desc = "New Weapon";

            SetUpgradeUI(property.Sprite, property.Name, desc, level);
            gameObject.SetActive(true);
        }

        public void Clicked()
        {
            OnClicked?.Invoke();
            if (_isUpgrade)
                PlayerManager.Instance.UpgradeWeapon(_weaponProperty);
            else
            {
                Weapon w;
                if (_weaponProperty.WeaponType == WeaponType.Area)
                {
                    w = WeaponManager.Instance.AddWeapon(PlayerManager.Instance.weaponParent,
                        _weaponProperty.AreaWeaponProperty.WeaponType,
                        WeaponTarget.Enemy);
                }
                else
                {
                    w = WeaponManager.Instance.AddWeapon(PlayerManager.Instance.weaponParent,
                        _weaponProperty.TargetBaseWeaponProperty.WeaponType, true);
                }

                Player.PlayerManager.Instance.AddWeapon(w);
            }

            _isUpgrade = false;
            gameObject.SetActive(false);
        }
    }
}