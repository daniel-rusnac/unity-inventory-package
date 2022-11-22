using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Static Item", fileName = "item_")]
    public class StaticItem : ScriptableObject, IItem
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;

        public ItemID ID => new(_id);
        public string Name => _name;
    }
}