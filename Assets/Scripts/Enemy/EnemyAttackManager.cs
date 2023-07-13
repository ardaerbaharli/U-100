using UnityEngine;
using Weapons;

namespace Enemy
{
    public class EnemyAttackManager : MonoBehaviour
    {
        private EnemyController _enemyController;
        private EnemyProperty _enemyProperty;

        private TargetBaseWeapon _weapon;


        private void Awake()
        {
            _enemyController = GetComponent<EnemyController>();

            _enemyController.OnPropertySet += SetEnemyProperty;
        }


        private void SetEnemyProperty(EnemyProperty obj)
        {
            _enemyProperty = obj;
            _weapon = GetComponentInChildren<TargetBaseWeapon>();
            _weapon.TargetInRangeChanged += OnTargetInRangeChanged;
        }

        private void OnTargetInRangeChanged(bool value)
        {
            _enemyController.isFollowingPlayer = !value;
        }
    }
}