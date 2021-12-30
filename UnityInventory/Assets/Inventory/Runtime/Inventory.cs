using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        public event Action onChanged;
        
        private Dictionary<byte, Slot> slotByID = new Dictionary<byte, Slot>();

        public void Add(byte id, int amount)
        {
            if (amount <= 0)
                return;
            
            if (!slotByID.ContainsKey(id))
            {
                slotByID.Add(id, new Slot());
            }

            slotByID[id] += amount;
            onChanged?.Invoke();
        }

        public void Remove(byte id, int amount)
        {
            if (amount <= 0 || !slotByID.ContainsKey(id))
                return;
            
            slotByID[id] -= amount;

            onChanged?.Invoke();
        }

        public bool Contains(byte id, int amount = 1)
        {
            if (amount <= 0)
                return true;

            return slotByID.ContainsKey(id) && slotByID[id].Count >= amount;
        }

        public int GetCountByID(byte id)
        {
            if (slotByID.ContainsKey(id))
            {
                return slotByID[id].Count;
            }

            return 0;
        }

        public int GetActiveSlotsCount()
        {
            return slotByID.Count(pair => pair.Value.Count > 0);
        }

        public int GetMax(byte id)
        {
            if (slotByID.ContainsKey(id))
            {
                return slotByID[id].Count;
            }

            return -1;
        }

        public void SetMax(byte id, int max)
        {
            if (!slotByID.ContainsKey(id))
            {
                slotByID.Add(id, new Slot(id, 0));
            }

            slotByID[id].SetMax(max);
            onChanged?.Invoke();
           
        }

        public void Clear()
        {
            slotByID.Clear();
            onChanged?.Invoke();
        }
    }
}