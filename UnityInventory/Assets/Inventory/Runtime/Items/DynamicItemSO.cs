namespace InventorySystem
{
    public abstract class DynamicItemSO : StaticItemSO
    {
        private int _dynamicID;

        public override int DynamicID => _dynamicID;

        protected abstract void OnInstanceCreated(DynamicItemSO instance);

        protected abstract ItemData OnSerialize();
        protected abstract void OnDeserialize(ItemData data);

        protected override ItemSO OnGetInstance()
        {
            DynamicItemSO item = Instantiate(this);
            item._dynamicID = InventoryUtility.GetID();
            OnInstanceCreated(item);
            return item;
        }

        public override ItemData Serialize()
        {
            ItemData data = OnSerialize();
            data.DynamicID = DynamicID;

            return data;
        }

        public override ItemSO Deserialize(ItemData data)
        {
            DynamicItemSO item = Instantiate(this);
            item.IsInstance = true;
            item._dynamicID = InventoryUtility.GetID();
            item.OnDeserialize(data);

            return item;
        }
    }
}