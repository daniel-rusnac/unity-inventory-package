using System;
using System.Collections.Generic;
using System.Linq;
using Items.Modules;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Item", fileName = "item_")]
    public class Item : ScriptableObject, IItem, ISerializationCallbackReceiver
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private ItemModulePair[] _modules = Array.Empty<ItemModulePair>();

        private Dictionary<string, ItemModuleBase> _moduleByKey = new();

        public ItemID ID => new(_id);
        public string Name => _name;

        public T GetModule<T>(string id) where T : class, IItemModule
        {
            if (_moduleByKey.ContainsKey(id))
                return default;

            return _moduleByKey[id] as T;
        }

        public void OnBeforeSerialize()
        {
            _modules = _moduleByKey.Select(pair => new ItemModulePair(pair.Key, pair.Value)).ToArray();
        }

        public void OnAfterDeserialize()
        {
            _moduleByKey = new Dictionary<string, ItemModuleBase>();
            
            foreach (ItemModulePair modulePair in _modules)
            {
                string key = modulePair.Key;

                if (_moduleByKey.ContainsKey(key))
                {
                    string modifiedKey = $"{key}_copy";
                
                    while (_moduleByKey.ContainsKey(modifiedKey))
                        modifiedKey = key + "_copy";

                    key = modifiedKey;
                }
                
                _moduleByKey.Add(key, modulePair.Module);
            }
        }

        [Serializable]
        private struct ItemModulePair
        {
            public string Key;
            public ItemModuleBase Module;

            public ItemModulePair(string key, ItemModuleBase module)
            {
                Key = key;
                Module = module;
            }
        }
    }
}