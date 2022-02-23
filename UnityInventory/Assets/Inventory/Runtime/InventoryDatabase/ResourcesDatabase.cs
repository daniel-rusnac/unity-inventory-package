using UnityEngine;

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
                if (ItemByID.ContainsKey(t.DynamicID))
                {
                    Debug.LogWarning($"Item with ID: [{t.DynamicID}] already registered!", t);
                    continue;
                }
                
                ItemByID.Add(t.DynamicID, t);
            }

            return true;
        }
    }
}