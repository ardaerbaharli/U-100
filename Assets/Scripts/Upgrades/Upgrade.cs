using System;
using UnityEngine;

namespace Upgrades
{
    [Serializable]
    public class Upgrade
    {
        public Action OnSelected;
        public Sprite Sprite;
        public string Title;
        public string Description;
        public int Level;
        public int MaxLevel;
        
    }
}