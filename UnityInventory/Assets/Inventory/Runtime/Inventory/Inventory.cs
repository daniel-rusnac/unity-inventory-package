using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private HashSet<Action> _onChangedSubscribers = new HashSet<Action>();
        // marked [SerializeField] for ES3 to auto save this class
        [SerializeField] private Dictionary<int, Slot> _slotByID = new Dictionary<int, Slot>();

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

        public void Add(int id, int amount)
        {
            if (amount <= 0)
                return;

            if (!_slotByID.ContainsKey(id))
            {
                _slotByID.Add(id, new Slot(id, 0));
            }

            _slotByID[id] += amount;
            OnChanged();
        }

        public void Remove(ItemSO item, int amount)
        {
            Remove(item.ID, amount);
        }

        public void Remove(int id, int amount)
        {
            if (amount <= 0 || !_slotByID.ContainsKey(id))
                return;

            _slotByID[id] -= amount;
            OnChanged();
        }

        /// <summary>
        /// Check if the inventory contains an item.
        /// </summary>
        /// <param name="item">Item to check.</param>
        /// <param name="amount">The expected amount.</param>
        /// <returns>True if the item count is >= to the expected amount. 1 by default.</returns>
        public bool Contains(ItemSO item, int amount = 1)
        {
            return Contains(item.ID, amount);
        }

        /// <summary>
        /// Check if the inventory contains an item.
        /// </summary>
        /// <param name="id">Item ID to check.</param>
        /// <param name="amount">The expected amount.</param>
        /// <returns>True if the item count is >= to the expected amount. 1 by default.</returns>
        public bool Contains(int id, int amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(id) && _slotByID[id].Count >= amount;
        }

        public int GetCount(ItemSO itemSO)
        {
            return GetCount(itemSO.ID);
        }

        public int GetCount(int id)
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

        public int GetMax(int id)
        {
            if (_slotByID.ContainsKey(id))
            {
                return _slotByID[id].Count;
            }

            return InventoryUtility.DEFAULT_MAX;
        }

        /// <summary>
        /// Set the maximum value for an item.
        /// </summary>
        /// <param name="itemSO">Item to set maximum valur for.</param>
        /// <param name="max">The maximum value. -1 is unlimited.</param>
        public void SetMax(ItemSO itemSO, int max)
        {
            SetMax(itemSO.ID, max);
        }

        /// <summary>
        /// Set the maximum value for an item.
        /// </summary>
        /// <param name="id">Item ID to set maximum valur for.</param>
        /// <param name="max">The maximum value. -1 is unlimited.</param>
        public void SetMax(int id, int max)
        {
            if (!_slotByID.ContainsKey(id))
            {
                _slotByID.Add(id, new Slot(id, 0, max));
            }
            else
            {
                _slotByID[id] = _slotByID[id].SetMax(max);
            }

            OnChanged();
        }

        public int[] GetAllIDs()
        {
            return _slotByID.Where(pair => pair.Value.Count > 0).Select(pair => pair.Key).ToArray();
        }

        public ItemSO[] GetAllItems()
        {
            return _slotByID.Where(pair => pair.Value.Count > 0).Select(pair =>
            {
                InventoryUtility.TryGetItem(pair.Key, out ItemSO item);
                return item;
            }).ToArray();
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
            int[] ids = _slotByID.Keys.Select(id => id).ToArray();
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

            _slotByID = new Dictionary<int, Slot>();

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

        /// <summary>
        /// Subscribe to the OnChanged event. Called every time the inventory is modified.
        /// </summary>
        public void Register(Action action)
        {
            _onChangedSubscribers.Add(action);
        }

        /// <summary>
        /// Unsubscribe from the OnChanged event. Called every time the inventory is modified.
        /// </summary>
        public void Unregister(Action action)
        {
            _onChangedSubscribers.Remove(action);
        }

        public override string ToString()
        {
            if (_slotByID.Count == 0)
                return "Empty";

            string result = "[";

            result += string.Join("] [", _slotByID.Values.Select(slot => slot.ToString()).ToArray());

            return result + "]";
        }
    }
}