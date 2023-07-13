using Enemy;
using UnityEngine;

namespace WaveDesign
{
    [CreateAssetMenu(fileName = "WaveProperty", menuName = "WaveProperty", order = 0)]
    public class WaveProperty : ScriptableObject
    {
        public float SpawnInterval;

        public DictionaryUnity<EnemyProperty, int> Enemies;
        // TODO: Add wave over event
    }
}