using UnityEngine;

namespace Items
{
    public interface IItem
    {
        ItemID ID { get; }
        string Name { get; }
        Sprite Icon { get; }
    }
}