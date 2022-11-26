namespace Items
{
    public interface ISlot
    {
        int Limit { get; set; }
        int Amount { get; set; }
        IItem Item { get; }
    }
}