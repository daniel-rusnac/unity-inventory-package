using System;
using UnityEngine;

namespace InventoriesDebug
{
    [Serializable]
    public class PlayerSkin
    {
        public string Name;
        [TextArea]
        public string Description;
        public GameObject SkinPrefab;
        public ParticleSystem DeathVfxPrefab;
        [Range(4, 12)]
        public int Height;
        [Range(1, 5)]
        public int Rarity;
    }
}