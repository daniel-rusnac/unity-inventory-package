using UnityEngine;

namespace InventorySystem.Database
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
                    if (ItemByID.ContainsKey(t.StaticID))
                    {
                        Debug.LogWarning($"Item with ID: [{t.StaticID}] already registered!", t);
                        continue;
                    }

                    ItemByID.Add(t.StaticID, t);
                }
            }

            return true;
        }
    }
}