using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
    public class InventorySO : ScriptableObject
    {
        [Obsolete("Use Register/Unregister instead.")]
        public event Action Changed;
        [Obsolete("Use Register/Unregister instead.")]
        public event Action<IItem, long> ChangedAmount;

        private HashSet<Action> _onChangedActions = new HashSet<Action>();
        private HashSet<Action<IItem, long>> _onChangedDeltaActions = new HashSet<Action<IItem, long>>();
        private readonly Dictionary<IItem, Slot> _slotByID = new Dictionary<IItem, Slot>();
        
        public void Add(IItem item, long amount, bool invokeActions = true)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                Remove(item, -amount, invokeActions);
            }

            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            long oldAmount = _slotByID[item].Amount; 
            _slotByID[item].Add(amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta, invokeActions);
        }
        
        public void Remove(IItem item, long amount, bool invokeActions = true)
        {
            if (amount == 0)
                return;

            if (amount < 0)
            {
                Add(item, -amount, invokeActions);
                return;
            }
            
            if (!_slotByID.ContainsKey(item))
                return;

            long oldAmount = _slotByID[item].Amount; 
            _slotByID[item].Remove(amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta, invokeActions);
        }

        public void SetAmount(IItem item, long amount, bool invokeActions = true)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0));
            }

            long oldAmount = _slotByID[item].Amount;
            _slotByID[item].Add(amount - _slotByID[item].Amount);
            long delta = _slotByID[item].Amount - oldAmount;
            
            OnChanged(item, delta, invokeActions);
        }

        public void SetLimit(IItem item, long max, bool invokeActions = true)
        {
            if (!_slotByID.ContainsKey(item))
            {
                _slotByID.Add(item, new Slot(item.ID, 0, max));
            }
            else
            {
                long oldAmount = _slotByID[item].Amount; 
                _slotByID[item].SetLimit(max);
                long delta = _slotByID[item].Amount - oldAmount;

                OnChanged(item, delta, invokeActions);
            }
        }

        /// <summary>
        /// Removes all items from the inventory.
        /// </summary>
        public void Clear(bool invokeActions = true)
        {
            foreach (IItem item in _slotByID.Keys)
            {
                OnChanged(item, -_slotByID[item].Amount, invokeActions);
            }
            
            _slotByID.Clear();
        }

        public bool Contains(IItem item, long amount = 1)
        {
            if (amount <= 0)
                return true;

            return _slotByID.ContainsKey(item) && _slotByID[item].Amount >= amount;
        }

        public long GetAmount(IItem item)
        {
            if (_slotByID.ContainsKey(item))
            {
                return _slotByID[item].Amount;
            }

            return 0;
        }

        /// <returns>The sum of all amounts.</returns>
        public long GetTotalAmount()
        {
            return _slotByID.Values.Sum(slot => slot.Amount);
        }

        public long GetLimit(IItem item)
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

        public IItem[] GetItems()
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key).ToArray();
        }

        /// <summary>
        /// Return all items of type T.
        /// </summary>
        public T[] GetItems<T>() where T : class, IItem
        {
            return _slotByID.Where(pair => pair.Value.Amount > 0).Select(pair => pair.Key as T).Where(item => item != null).ToArray();
        }

        public void RemoveLimit(IItem item, bool invokeActions = true)
        {
            SetLimit(item, -1, invokeActions);
        }

        [Obsolete("Use SetLimit() instead.")]
        public void SetMax(IItem item, long max, bool invokeActions = true)
        {
            SetLimit(item, max, invokeActions);
        }

        [Obsolete("Use GetLimit() instead.")]
        public long GetMax(IItem item)
        {
            return GetLimit(item);
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

            int[] ids = values[0].Split(',').Select(int.Parse).ToArray();
            long[] amount = values[1].Split(',').Select(long.Parse).ToArray();
            int[] max = values[2].Split(',').Select(int.Parse).ToArray();

            int slotCount = ids.Length;

            for (int i = 0; i < slotCount; i++)
            {
                if (InventoryUtility.TryGetItem(ids[i], out IItem item))
                {
                    if (!_slotByID.ContainsKey(item))
                    {
                        Add(item, amount[i], false);
                        SetLimit(item, max[i], false);
                    }
                    else
                    {
                        SetLimit(item, max[i], false);
                        SetAmount(item, amount[i], false);
                    }
                }
                else
                {
                    Debug.LogWarning($"Couldn't deserialize id: {ids[i]}! Maybe you changed it after saving?", this);
                }
            }
        }
        
        public void Register(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Register(Action<IItem, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }
        
        public void Unregister(Action action)
        {
            _onChangedActions.Add(action);   
        }

        public void Unregister(Action<IItem, long> action)
        {
            _onChangedDeltaActions.Add(action);   
        }

        private void OnChanged(IItem item, long delta, bool invokeActions)
        {
            if (!invokeActions || delta == 0)
                return;
            
            foreach (Action<IItem, long> action in _onChangedDeltaActions)
            {
                action.Invoke(item, delta);
            }

#pragma warning disable CS0618
            ChangedAmount?.Invoke(item, delta);
#pragma warning restore CS0618
            OnChanged();
        }

        private void OnChanged()
        {
            foreach (Action action in _onChangedActions)
            {
                action.Invoke();
            }
            
#pragma warning disable CS0618
            Changed?.Invoke();
#pragma warning restore CS0618
        }
    }
}