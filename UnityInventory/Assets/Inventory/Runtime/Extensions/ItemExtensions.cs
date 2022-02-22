using System.Linq;

namespace InventorySystem
{
    public static class ItemExtensions
    {
        public static bool IsDynamic(this ItemSO item)
        {
            return item.GetType().GetInterfaces().Any(t =>
                t.IsGenericType &&
                t.GetGenericTypeDefinition() == typeof(IDynamicItem<>));
        }
    }
}