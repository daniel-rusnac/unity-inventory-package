using UnityEngine;

namespace FoggyWoods.Inventories.Items
{
    [CreateAssetMenu(menuName = "Inventories/Item")]
    public class ItemDefinition : ScriptableObject, IItem
    {
        public string ID => name;
    }
}