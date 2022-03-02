namespace InventorySystem
{
    public abstract class DynamicItemSO : StaticItemSO
    {
        private int _dynamicID = -1;

        public override int DynamicID => _dynamicID;

        /// <summary>
        /// Called when a new instance of this item was created. Use it to setup custom data.
        /// </summary>
        /// <param name="instance">The new item instance.</param>
        protected abstract void OnInstanceCreated(DynamicItemSO instance);

        protected abstract object OnSerialize();
        
        protected abstract void OnDeserialize(object data);

        protected override ItemSO OnGetInstance()
        {
            DynamicItemSO item = Instantiate(this);
            item._dynamicID = InventoryUtility.GetID();
            OnInstanceCreated(item);
            return item;
        }

        public override object Serialize()
        {
            return OnSerialize();
        }

        public override ItemSO Deserialize(int dynamicID, object data)
        {
            if (InventoryUtility.TryGetItem(dynamicID, out ItemSO existingItem))
            {
                if (existingItem.IsDynamic)
                {
                    ((DynamicItemSO)existingItem).OnDeserialize(data);
                }
                
                return existingItem;
            }

            DynamicItemSO item = Instantiate(this);
            item.IsInstance = true;
            item._dynamicID = dynamicID;
            item.OnDeserialize(data);
            InventoryUtility.AddItemToDatabase(item);

            return item;
        }
    }
}