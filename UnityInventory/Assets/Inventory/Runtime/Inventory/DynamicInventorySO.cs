using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Dynamic Inventory", menuName = InventoryConstants.CREATE_INVENTORY_SUB_MENU + "Dynamic Inventory")]
    public class DynamicInventorySO : InventorySO
    {
        private readonly Dictionary<IDynamicItem<IDynamicData>, Dictionary<int, IDynamicData>> _instanceByDynamicID = 
            new Dictionary<IDynamicItem<IDynamicData>, Dictionary<int, IDynamicData>>();

        public void Add<T>(IDynamicItem<T> item, long amount) where T : IDynamicData
        {
            Debug.Log("Dynamic");
            IDynamicItem<IDynamicData> castItem = (IDynamicItem<IDynamicData>) item;
            
            if (_instanceByDynamicID.ContainsKey(castItem))
            {
                _instanceByDynamicID.Add(castItem, new Dictionary<int, IDynamicData>());
            }

            for (int i = 0; i < amount; i++)
            {
                int id = InventoryUtility.GetID();

                while (_instanceByDynamicID[castItem].ContainsKey(id))
                {
                    id = InventoryUtility.GetID();
                }
            
                _instanceByDynamicID[castItem].Add(id, item.CreateDataInstance);
            }
        }

        public T GetInstance<T>(IDynamicItem<T> item, int id) where T : IDynamicData
        {
            IDynamicItem<IDynamicData> castItem = (IDynamicItem<IDynamicData>) item;

            if (!_instanceByDynamicID.ContainsKey(castItem))
                return default;

            if (!_instanceByDynamicID[castItem].ContainsKey(id))
                return default;

            return (T) _instanceByDynamicID[castItem][id];
        }
    }
}