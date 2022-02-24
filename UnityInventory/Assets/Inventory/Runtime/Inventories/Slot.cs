namespace InventorySystem
{
    public class Slot
    {
        private int _staticID;
        private int _dynamicID;

        public int StaticID => _staticID;
        public int DynamicID => _dynamicID;
        public long Amount { get; set; }

        public Slot(int staticID, int dynamicID)
        {
            _staticID = staticID;
            _dynamicID = dynamicID;
        }
    }
}