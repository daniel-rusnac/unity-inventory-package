using UnityEngine;

namespace ItemManagement
{
    public class Item : IItem
    {
        public IItemDefinition Definition { get; }

        public int ID => Definition.ID;
        public string Name => Definition.Name;
        public Sprite Icon => Definition.Icon;

        public Item(IItemDefinition itemDefinition)
        {
            Definition = itemDefinition;
        }
    }
}