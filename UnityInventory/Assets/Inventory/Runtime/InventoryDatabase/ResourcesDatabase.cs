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
                if (ItemByID.ContainsKey(t.StaticID))
                {
                    Debug.LogWarning($"Item with ID: [{t.StaticID}] already registered!", t);
                    continue;
                }
                
                ItemByID.Add(t.StaticID, t);
            }

            return true;
        }
    }
}