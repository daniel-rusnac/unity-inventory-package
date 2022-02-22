namespace InventorySystem
{
    public interface IDynamicData
    {
        
    }
    
    public interface IDynamicItem<T> : IDynamicItemBase where T : IDynamicData
    {
        public T CreateDataInstance { get; }
    }
}