using UnityEngine;

namespace InventorySystem.InventoryDatabase
{
    public class ReferenceDatabase : SceneDatabase
    {
        [SerializeField] private ReferenceDatabaseCollectionSO[] _collections;

        protected override bool OnInitialize()
        {
            foreach (ReferenceDatabaseCollectionSO collection in _collections)
            {
                foreach (ItemSO t in collection.Items)
                {
                    if (ItemByID.ContainsKey(t.ID))
                    {
                        Debug.LogWarning($"Item with ID: [{t.ID}] already registered!", t);
                        continue;
                    }

                    ItemByID.Add(t.ID, t);
                }
            }

            return true;
        }
    }
}