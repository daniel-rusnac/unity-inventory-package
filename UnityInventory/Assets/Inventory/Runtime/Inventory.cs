using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private HashSet<Action> _onChangedSubscribers = new HashSet<Action>();
        private Dictionary<byte, Slot> _slotByID = new Dictionary<byte, Slot>();

        public Inventory()
        {
            
        }
        
        public Inventory(object serializedData)
        {
            Deserialize(serializedData);   
        }

        public void Add(ItemSO item, int amount)
        {
            Add(item.ID, amount);
        }

        public void Add(byte id, int amount)
        {
            if (amount <= 0)
                return;

            if (!_slotByID.ContainsKey(id))
            {
                _slotByID.Add(id, new Slot());
            }

            _slotByID[id] += amount;
            OnChanged();
        }

        public void Remove(ItemSO item, int amount)
        {
            Remove(item.ID, amount);
        }
        public void Remove(byte id, int amount)
        {
            if (amount <= 0 || !_slotByID.ContainsKey(id))
                return;

            _slotByID[id] -= amount;
            OnChanged();
        }

        public bool Contains(ItemSO item, int amount = 1)
        {
            return Contains(item.ID, amount);
        }

        public bool Contains(byte id, int amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(id) && _slotByID[id].Count >= amount;
        }

        public int GetCount(ItemSO itemSO)
        {
            return GetCount(itemSO.ID);
        }
        
        public int GetCount(byte id)
        {
            if (_slotByID.ContainsKey(id))
            {
                return _slotByID[id].Count;
            }

            return 0;
        }

        public int GetMax(ItemSO itemSO)
        {
            return GetMax(itemSO.ID);
        }
        
        public int GetMax(byte id)
        {
            if (_slotByID.ContainsKey(id))
            {
                return _slotByID[id].Count;
            }

            return InventoryUtility.DEFAULT_MAX;
        }

        public void SetMax(ItemSO itemSO, int max)
        {
            SetMax(itemSO.ID, max);
        }

        public void SetMax(byte id, int max)
        {
            if (!_slotByID.ContainsKey(id))
            {
                _slotByID.Add(id, new Slot(id, 0));
            }

            _slotByID[id].SetMax(max);
            OnChanged();
        }

        public int GetSlotCount()
        {
            return _slotByID.Count(pair => pair.Value.Count > 0);
        }

        public void Clear()
        {
            _slotByID.Clear();
            OnChanged();
        }

        public object Serialize()
        {
            byte[] ids = _slotByID.Keys.Select(id => id).ToArray();
            Slot[] counts = _slotByID.Values.Select(slot => slot).ToArray();

            object[] result =
            {
                ids,
                counts
            };

            return result;
        }

        public void Deserialize(object data)
        {
            byte[] ids = (byte[]) ((object[]) data)[0];
            Slot[] slots = (Slot[]) ((object[]) data)[1];

            _slotByID = new Dictionary<byte, Slot>();

            for (int i = 0; i < ids.Length; i++)
            {
                _slotByID.Add(ids[i], slots[i]);
            }
        }

        private void OnChanged()
        {
            foreach (Action subscriber in _onChangedSubscribers)
            {
                subscriber?.Invoke();
            }
        }

        public void Register(Action action)
        {
            _onChangedSubscribers.Add(action);
        }

        public void Unregister(Action action)
        {
            _onChangedSubscribers.Remove(action);
        }
    }
}