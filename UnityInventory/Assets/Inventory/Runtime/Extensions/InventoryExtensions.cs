using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace InventorySystem
{
    public static class InventoryExtensions
    {
        /// <summary>
        /// Removed the items from an inventory and ads them to another.
        /// </summary>
        public static void TransferTo(this InventorySO from, InventorySO to, ItemSO item, long amount = 1)
        {
            long oldAmount = from.GetAmount(item);
            from.Remove(item, amount);
            to.Add(item, oldAmount - from.GetAmount(item));
        }

        public static void Save(this InventorySO inventory, string saveKey)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + $"/{saveKey}.inv");
            bf.Serialize(file, inventory.Serialize());
            file.Close();
        }

        public static void Load(this InventorySO inventory, string saveKey)
        {
            InventoryData data = new InventoryData();

            if (File.Exists(GetInventorySavePath(saveKey)))
            {
                FileStream file = File.Open(GetInventorySavePath(saveKey), FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                data = (InventoryData)bf.Deserialize(file);
                file.Close();
            }

            inventory.Deserialize(data);
        }

        private static string GetInventorySavePath(string saveKey)
        {
            return Application.persistentDataPath + $"/{saveKey}.inv";
        }
    }
}