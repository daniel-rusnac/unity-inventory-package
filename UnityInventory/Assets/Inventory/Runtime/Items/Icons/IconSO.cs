using System;
using UnityEngine;

namespace InventorySystem.Icons
{
    public abstract class IconSO : ScriptableObject
    {
        public abstract Sprite GetIcon();
        public abstract Sprite GetIcon<T>(T type) where T : Enum;
    }
}