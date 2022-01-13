using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        private HashSet<Action> onChagedSubscribers = new HashSet<Action>();
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
            OnChanged();
        }

        public void Remove(byte id, int amount)
        {
            if (amount <= 0 || !slotByID.ContainsKey(id))
                return;

            slotByID[id] -= amount;
            OnChanged();
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

            return InventoryUtility.DEFAULT_MAX;
        }

        public void SetMax(byte id, int max)
        {
            if (!slotByID.ContainsKey(id))
            {
                slotByID.Add(id, new Slot(id, 0));
            }

            slotByID[id].SetMax(max);
            OnChanged();
        }

        public void Clear()
        {
            slotByID.Clear();
            OnChanged();
        }

        public object Serialize()
        {
            byte[] ids = slotByID.Keys.Select(id => id).ToArray();
            Slot[] counts = slotByID.Values.Select(slot => slot).ToArray();

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

            slotByID = new Dictionary<byte, Slot>();

            for (int i = 0; i < ids.Length; i++)
            {
                slotByID.Add(ids[i], slots[i]);
            }
        }

        private void OnChanged()
        {
            foreach (Action subscriber in onChagedSubscribers)
            {
                subscriber?.Invoke();
            }
        }

        public void Register(Action action)
        {
            onChagedSubscribers.Add(action);
        }

        public void Unregister(Action action)
        {
            onChagedSubscribers.Remove(action);
        }
    }
}