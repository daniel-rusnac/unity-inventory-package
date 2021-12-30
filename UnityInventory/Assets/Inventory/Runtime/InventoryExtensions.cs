// using System.Collections.Generic;
// using UnityEngine;
//
// namespace TryMyGames.Snake2048.InventorySystem
// {
//     public static class InventoryExtensions
//     {
//         private const float TRANSFER_DURATION = 1f;
//
//         public static void TransferImmediate(this Inventory from, string itemID, Inventory to, int amount,
//             int multiplier = 1)
//         {
//             int transferAmount = Mathf.Min(amount, from.GetCount(itemID));
//
//             from.Remove(itemID, transferAmount);
//             to.Add(itemID, transferAmount * multiplier);
//         }
//
//         public static async UniTask Transfer(this Inventory from, string itemID, Inventory to, int amount,
//             int multiplier = 1, float duration = TRANSFER_DURATION)
//         {
//             if (amount == 0)
//                 return;
//
//             if (duration <= 0f)
//             {
//                 from.TransferImmediate(itemID, to, amount, multiplier);
//                 return;
//             }
//
//             float time = 0f;
//             int transferredItems = 0;
//
//             while (time < duration)
//             {
//                 time += Time.deltaTime;
//                 int tickAmount = 0;
//
//                 while ((float)(transferredItems + tickAmount) / amount < time / duration)
//                 {
//                     tickAmount++;
//                 }
//
//                 if (tickAmount > 0)
//                 {
//                     from.TransferImmediate(itemID, to, tickAmount, multiplier);
//                     transferredItems += tickAmount;
//                 }
//
//                 await UniTask.Yield();
//             }
//         }
//
//         public static async UniTask TransferAll(this Inventory from, string itemID, Inventory to, int multiplier = 1,
//             float duration = TRANSFER_DURATION)
//         {
//             await from.Transfer(itemID, to, from.GetCount(itemID), multiplier, duration);
//         }
//
//         public static async UniTask TransferAll(this Inventory from, Inventory to, int multiplier = 1,
//             float duration = TRANSFER_DURATION)
//         {
//             List<string> items = from.GetAllItemIDs();
//
//             if (items.Count == 0)
//                 return;
//
//             duration /= items.Count;
//
//             foreach (string item in items)
//             {
//                 await from.TransferAll(item, to, multiplier, duration);
//             }
//         }
//
//         public static async UniTask AddAsync(this Inventory to, string itemID, int amount,
//             float duration = TRANSFER_DURATION)
//         {
//             if (amount == 0)
//                 return;
//
//             float time = 0f;
//             int transferredItems = 0;
//
//             while (time < duration)
//             {
//                 time += Time.deltaTime;
//                 int tickAmount = 0;
//
//                 while ((float)(transferredItems + tickAmount) / amount < time / duration)
//                 {
//                     tickAmount++;
//                 }
//
//                 if (tickAmount > 0)
//                 {
//                     transferredItems += tickAmount;
//                     to.Add(itemID, tickAmount);
//                 }
//
//                 await UniTask.Yield();
//             }
//         }
//     }
// }