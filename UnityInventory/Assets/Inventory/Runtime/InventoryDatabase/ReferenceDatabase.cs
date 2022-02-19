using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class ReferenceDatabase : SceneDatabase
    {
        [SerializeField] private ItemSO[] _items;

        protected override bool OnInitialize()
        {
            foreach (ItemSO t in _items)
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

        [ContextMenu("Load All Items From Assets")]
        private void LoadAllItemsFromAssets()
        {
            
        }
    }
}