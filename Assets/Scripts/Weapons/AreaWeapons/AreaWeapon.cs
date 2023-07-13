using NaughtyAttributes;

namespace Weapons
{
    public abstract class AreaWeapon : Weapon
    {
        public int MaxLevel;
        public int Level;
        public float Damage;
        public AreaWeaponType WeaponType;
        public WeaponTarget WeaponTarget;

        [Button]
        public void Upgrade()
        {
            if (MaxLevel <= Level) return;

            Level++;
            Upgraded();
        }

        public abstract void Upgraded();

        public void SetProperties(WeaponProperty properties, WeaponTarget weaponTarget)
        {
            Damage = properties.AreaWeaponProperty.Damage;
            WeaponType = properties.AreaWeaponProperty.WeaponType;
            WeaponTarget = weaponTarget;
            StartAttack();
        }

        public abstract void StartAttack();
    }
}