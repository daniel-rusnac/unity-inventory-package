using UnityEngine;

namespace ItemManagement
{
    public interface IItemDefinition
    {
        int ID { get; }
        string Name { get; }
        Sprite Icon { get; }
    }
}