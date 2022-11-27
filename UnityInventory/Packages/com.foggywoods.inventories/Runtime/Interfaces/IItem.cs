namespace FoggyWoods.Inventories
{
    public interface IItem
    {
        string ID { get; }

        T GetProperty<T>(string key);
        bool TryGetProperty<T>(string key, out T property);
    }
}