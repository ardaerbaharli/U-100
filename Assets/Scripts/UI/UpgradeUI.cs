using System;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;
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
        public Action<Upgrade> OnClicked;
        private Upgrade upgrade;

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

        
        public void SetData(Upgrade u)
        {
            SetUpgradeUI(u.Sprite, u.Title, u.Description, u.Level);
            upgrade = u;
            gameObject.SetActive(true);
        }


        public void Clicked()
        {
            OnClicked?.Invoke(upgrade);
            gameObject.SetActive(false);
        }
    }
}