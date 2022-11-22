using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Item", fileName = "item_")]
    public class Item : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
    }
}