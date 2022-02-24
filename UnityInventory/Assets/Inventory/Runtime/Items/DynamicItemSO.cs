namespace InventorySystem
{
    public abstract class DynamicItemSO : StaticItemSO
    {
        private int _dynamicID;

        public override int DynamicID => _dynamicID;
        
        protected override ItemSO OnGetInstance()
        {
            DynamicItemSO item = Instantiate(this);
            item._dynamicID = InventoryUtility.GetID();
            OnInstanceCreated(item);
            return item;
        }

        protected abstract void OnInstanceCreated(DynamicItemSO instance);
    }
}