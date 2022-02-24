namespace InventorySystem.Slots
{
    public class DynamicSlot : Slot
    {
        private int _staticID;
        private int _dynamicID;

        public override int StaticID => _staticID;
        public override int DynamicID => _dynamicID;
        public override long Amount { get; set; }

        public DynamicSlot(int staticID, int dynamicID)
        {
            _staticID = staticID;
            _dynamicID = dynamicID;
        }
    }
}