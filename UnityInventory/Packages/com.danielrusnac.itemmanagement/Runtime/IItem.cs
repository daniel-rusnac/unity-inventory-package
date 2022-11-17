using UnityEngine;

namespace ItemManagement
{
    public interface IItem
    {
        int ID { get; }
        string Name { get; }
        Sprite Icon { get; }
        IItemDefinition Definition { get; }
    }
}