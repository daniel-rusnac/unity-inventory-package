using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public abstract class InventorySO : ScriptableObject
    {
        private HashSet<Action> _onChangedActions = new HashSet<Action>();
        private HashSet<Action<ItemSO, long>> _onChangedDeltaActions = new HashSet<Action<ItemSO, long>>();

        public abstract void Add(ItemSO item, long amount = 1);
        public abstract void Remove(ItemSO item, long amount = 1);
        public abstract void SetAmount(ItemSO item, long amount);
        public abstract long GetAmount(ItemSO item);
        public abstract bool Contains(ItemSO item, long amount = 1);
        
        public abstract void SetLimit(ItemSO item, long limit);
        public abstract long GetLimit(ItemSO item);
        public abstract string Serialize();
        public abstract void Deserialize(string data);
        public abstract ItemSO[] GetInstances();
        public abstract T[] GetInstances<T>() where T : ItemSO;
        public abstract T GetInstance<T>(T item, int dynamicID) where T : ItemSO;
        public abstract T GetAnyInstance<T>(T item) where T : ItemSO;
        
        public void Register(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Register(Action<ItemSO, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }
        
        public void Unregister(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Unregister(Action<ItemSO, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }

        protected void OnChanged(ItemSO item, long delta)
        {
            if (delta == 0)
                return;
            
            foreach (Action<ItemSO, long> action in _onChangedDeltaActions)
            {
                action.Invoke(item, delta);
            }
            
            OnChanged();
        }
        
        private void OnChanged()
        {
            foreach (Action action in _onChangedActions)
            {
                action.Invoke();
            }
        }
    }
}