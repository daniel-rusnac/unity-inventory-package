using System.Linq;

namespace FoggyWoods.Inventories
{
    public static class InventoryExtensions
    {
        public static bool Contains(this IInventory inventory, IItem item, int amount = 1)
        {
            if (!inventory.ContainsSlot(item))
                return false;

            return inventory.GetOrCreateSlot(item).Amount >= amount;
        }

        public static void Add(this IInventory inventory, IItem item, int amount = 1)
        {
            inventory.GetOrCreateSlot(item).Amount += amount;
        }

        public static void Remove(this IInventory inventory, IItem item, int amount = 1)
        {
            if (!inventory.ContainsSlot(item))
                return;

            inventory.GetOrCreateSlot(item).Amount -= amount;
        }

        public static void SetAmount(this IInventory inventory, IItem item, int amount)
        {
            inventory.GetOrCreateSlot(item).Amount = amount;
        }

        public static int GetAmount(this IInventory inventory, IItem item)
        {
            if (!inventory.ContainsSlot(item))
                return 0;

            return inventory.GetOrCreateSlot(item).Amount;
        }

        public static void TransferAll(this IInventory from, IInventory to)
        {
            ISlot[] slots = from.Slots.ToArray();

            foreach (ISlot slot in slots)
            {
                to.Add(slot.Item, slot.Amount);
                from.Remove(slot.Item, slot.Amount);
            }
        }
    }
}