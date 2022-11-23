using Items;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _amount = 1;
    [SerializeField] private Item _item;
    [SerializeField] private Inventory _inventory;

    private void OnValidate()
    {
        if (Application.isPlaying)
            SetAmount();
    }

    private void OnEnable()
    {
        _inventory.Changed += OnInventoryChanged;
    }

    private void OnDisable()
    {
        _inventory.Changed -= OnInventoryChanged;
    }

    private void SetAmount()
    {
        _inventory.GetSlotOrCreate(_item).Amount = _amount;
    }

    private void OnInventoryChanged(ItemChangedData data)
    {
    }
}
