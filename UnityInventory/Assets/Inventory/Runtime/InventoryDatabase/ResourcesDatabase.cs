﻿using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class ResourcesDatabase : SceneDatabase
    {
        [SerializeField] private string _itemsPath = "";
        
        protected override bool OnInitialize()
        {
            ItemSO[] items = Resources.LoadAll<ItemSO>(_itemsPath);
            
            foreach (ItemSO t in items)
            {
                if (ItemByID.ContainsKey(t.ID))
                {
                    Debug.LogWarning($"Item with ID: [{t.ID}] already registered!", t);
                    continue;
                }
                
                ItemByID.Add(t.ID, t);
            }

            return true;
        }
    }
}