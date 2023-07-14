using NaughtyAttributes;

namespace Weapons
{
    public abstract class AreaWeapon : Weapon
    {
        public float Damage;
        public AreaWeaponType WeaponType;
        public WeaponTarget WeaponTarget;
        private AreaWeaponProperty _areaProperty;

        public override void Upgrade()
        {
            Level++;
            var data = _areaProperty.GetUpgradeData(Level);
            SetData(data);
            
            Upgraded();
        }

        private void SetData(AreaWeaponUpgradeData data)
        {
            Damage = data.Damage;
            UpgradeDescription = data.UpgradeDescription;
        }

        public abstract void Upgraded();

        public void SetWeapon(WeaponProperty property, WeaponTarget weaponTarget)
        {
            Property = property;
            
            Sprite = property.Sprite;
            Name = property.Name;
            
            Type = Weapons.WeaponType.Area;
            Level = 1;
            MaxLevel = property.MaxLevel;
            WeaponTarget = weaponTarget;

            _areaProperty = property.AreaWeaponProperty;
            AreaWeaponType = _areaProperty.WeaponType;
            WeaponType = _areaProperty.WeaponType;
            
            var data = _areaProperty.GetUpgradeData(Level);
            SetData(data);
            
            StartAttack();
        }


        public abstract void StartAttack();
    }
}