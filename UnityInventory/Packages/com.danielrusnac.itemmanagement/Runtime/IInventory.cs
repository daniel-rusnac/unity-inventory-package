using System;

namespace ItemManagement
{
    public interface IItemDefinition
    {
        int ID { get; }
        string Name { get; }   
    }
    
    public interface IItem
    {
        int ID { get; }
        string Name { get; }
    }
    
    public interface IInventorySlot
    {
        int Amount { get; set; }
    }
    
    public interface IInventory
    {
        event Action<IItemDefinition, int> Changed;
        IInventorySlot GetOrCreateSlot(IItemDefinition itemDefinition);
    }
}