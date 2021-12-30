using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        public event Action onChanged;

        [SerializeField] private List<Slot> slots;

        public void Add(string id, int amount)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    slots[i] += amount;
                    onChanged?.Invoke();
                    return;
                }
            }

            slots.Add(new Slot(id, amount, -1));
            onChanged?.Invoke();
        }

        public void Remove(string id, int amount)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    slots[i] -= amount;

                    if (slots[i].Count <= 0)
                    {
                        slots.RemoveAt(0);
                    }

                    onChanged?.Invoke();
                    break;
                }
            }
        }

        public bool Contains(string id, int amount = 1)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    return slots[i].Count >= amount;
                }
            }

            return false;
        }

        public int GetCount(string id)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    return slots[i].Count;
                }
            }

            return 0;
        }

        public int GetMax(string id)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    return slots[i].Max;
                }
            }

            return 0;
        }

        public void SetMax(string id, int max)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ID == id)
                {
                    slots[i] = new Slot(slots[i].ID, slots[i].Count, max);
                    onChanged?.Invoke();
                    return;
                }
            }

            slots.Add(new Slot(id, 0, max));
            onChanged?.Invoke();
        }

        public void Clear()
        {
            slots.Clear();
            onChanged?.Invoke();
        }

        public List<string> GetAllItemIDs()
        {
            List<string> result = new List<string>();

            foreach (Slot slot in slots)
            {
                if (slot.Count > 0)
                {
                    result.Add(slot.ID);
                }
            }

            return result;
        }
    }
}