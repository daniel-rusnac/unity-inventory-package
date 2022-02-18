using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemDataCollection
    {
        [SerializeField] private string[] _dataID;
        [SerializeField] private ItemDataBase[] _data;

        public string[] DataID => _dataID;

        public ItemDataBase[] Data => _data;
    }
    
    [CreateAssetMenu(fileName = "Composite Item", menuName = InventoryConstants.CREATE_ITEMS_SM + "Composite Item")]
    public class CompositeItem : ScriptableObject, IItem
    {
        [Tooltip("The ID is used by the inventory to store and load items. This must be unique!")]
        [SerializeField] private int _id;
        [SerializeField] private ItemDataCollection _staticData;

        private Dictionary<string, ItemDataBase> _dataByID;
        
        public int ID => _id;

        public string ItemName => GetData<string>("item_name");

        public T GetData<T>(string dataID)
        {
            return default;
        }

        public void SetData<T>(string dataID)
        {
            
        }

        internal void Initialize()
        {
            _dataByID = new Dictionary<string, ItemDataBase>();

            InitializeDataCollection(_staticData);
        }

        private void InitializeDataCollection(ItemDataCollection dataCollection)
        {
            
        }
    }
}