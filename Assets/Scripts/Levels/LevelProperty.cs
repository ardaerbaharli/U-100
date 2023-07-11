using System.Collections.Generic;
using UnityEngine;
using WaveDesign;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelProperty", menuName = "LevelProperty", order = 0)]
    public class LevelProperty : ScriptableObject
    {
        public List<WaveProperty> Waves;

        public float Interval;
        // TODO: Add level over event
    }
}