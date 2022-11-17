using UnityEngine;

namespace ItemManagement
{
    [CreateAssetMenu(menuName = "Item Management/Item Definition", fileName = "item_")]
    public class ItemDefinition : ScriptableObject, IItemDefinition
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}