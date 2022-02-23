using System.Collections.Generic;
using System.Linq;
using InventorySystem.New;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Legacy Inventory", menuName = InventoryConstants.CREATE_SO_MENU + "Legacy Inventory")]
    public class OldInventorySO : InventorySO
    {
        private readonly Dictionary<ItemSO, OldSlot> _slotByID = new Dictionary<ItemSO, OldSlot>();

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear(bool invokeActions = true)
        {
            foreach (ItemSO item in _slotByID.Keys)
            {
                OnChanged(item, -_slotByID[item].Amount);
            }
            
            _slotByID.Clear();
        }

        public bool Contains(ItemSO item, long amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(item) && _slotByID[item].Amount >= amount;
        }

        public override void AddAmount(ItemSO item, long amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                RemoveAmount(item, -amount);
            }

            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new OldSlot(item.ID, 0));
            }

            long oldAmount = _slotByID[item].Amount; 
            _slotByID[item].Add(amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta);
        }

        public override void RemoveAmount(ItemSO item, long amount)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                AddAmount(item, -amount);
                return;
            }
            
            if (!_slotByID.ContainsKey(item))
                return;

            long oldAmount = _slotByID[item].Amount; 
            _slotByID[item].Remove(amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta);
        }

        public override void SetAmount(ItemSO item, long amount)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new OldSlot(item.ID, 0));
            }

            long oldAmount = _slotByID[item].Amount;
            _slotByID[item].Add(amount - _slotByID[item].Amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta);
        }

        public override long GetAmount(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Amount;
            }

            return 0;
        }

        public override void SetLimit(ItemSO item, long limit)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new OldSlot(item.ID, 0, limit));
            }
            else
            {
                long oldAmount = _slotByID[item].Amount; 
                _slotByID[item].SetLimit(limit);
                long delta = _slotByID[item].Amount - oldAmount;

                OnChanged(item, delta);
            }
        }

        /// <returns>The sum of all amounts.</returns>
        public long GetTotalAmount()
        {
            return _slotByID.Values.Sum(slot => slot.Amount);
        }

        public override long GetLimit(ItemSO item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Limit;
            }

            return InventoryUtility.DEFAULT_LIMIT;
        }

        /// <returns>How many slots with count > 0 are in the inventory.</returns>
        public int GetActiveSlotsCount()
        {
            return _slotByID.Count(pair => pair.Value.Amount > 0);
        }

        public ItemSO[] GetItems()
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key).ToArray();
        }

        /// <summary>
        /// Return all items of type T.
        /// </summary>
        public T[] GetItems<T>() where T : ItemSO
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key as T).Where(item => item != null).ToArray();
        }

        public void RemoveLimit(ItemSO item, bool invokeActions = true)
        {
            SetLimit(item, -1);
        }

        public override string ToString()
        {
            if (_slotByID.Count == 0)
                return "[Empty]";

            string result = "[";

            result += string.Join("] [", _slotByID.Values.Select(slot => slot.ToString()).ToArray());

            return result + "]";
        }

        public string Serialize()
        {
            string[] values = 
            {
                string.Join(",", _slotByID.Keys.Select(so => so.ID)),
                string.Join(",", _slotByID.Values.Select(slot => slot.Amount)),
                string.Join(",", _slotByID.Values.Select(slot => slot.Limit))
            };
            
            return string.Join("|", values);
        }

        public void Deserialize(string content)
        {
            string[] values = content.Split('|');
            
            if (values.Length != 3)
            {
                Debug.LogWarning($"Couldn't deserialize content: {content}!", this);
                return;
            }
            
            if (string.IsNullOrWhiteSpace(values[0]) || values[0].Split(',').Length == 0)
            {
                return;
            }

            int[] ids = values[0].Split(',').Select(int.Parse).ToArray();
            long[] amount = values[1].Split(',').Select(long.Parse).ToArray();
            long[] max = values[2].Split(',').Select(long.Parse).ToArray();

            int slotCount = ids.Length;

            for (int i = 0; i < slotCount; i++)
            {
                if (InventoryUtility.TryGetItem(ids[i], out ItemSO item))
                {
                    if (!_slotByID.ContainsKey(item))
                    {
                        AddAmount(item, amount[i]);
                        SetLimit(item, max[i]);
                    }
                    else
                    {
                        SetLimit(item, max[i]);
                        SetAmount(item, amount[i]);
                    }
                }
                else
                {
                    Debug.LogWarning($"Couldn't deserialize id: {ids[i]}! Maybe you changed it after saving?", this);
                }
            }
        }
    }
}