namespace InventorySystem
{
    public interface IDynamicData
    {
        
    }
    
    public interface IDynamicItem<T> where T : IDynamicData
    {
        public T CreateDataInstance { get; }
    }
}