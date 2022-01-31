using System;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private Inventory _inventory;

        [Obsolete("Use GetInventory instead.")]
        public Inventory Value => _inventory;
        
        public Inventory GetInventory => _inventory;
    }
}