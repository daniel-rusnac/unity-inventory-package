using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Item", fileName = "item_")]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private List<ItemModule> _modules = new();

        public int ID => _id;
        public List<ItemModule> Modules => _modules;

        public void AddModule(ItemModule module)
        {
            _modules.Add(module);
        }

        public void RemoveModule(ItemModule module)
        {
            _modules.Remove(module);
        }

        public T GetModule<T>(string key) where T : ItemModule
        {
            foreach (ItemModule module in _modules)
            {
                if (string.Equals(module.Key, key) && module is T itemModule)
                    return itemModule;
            }

            return default;
        }
    }
}