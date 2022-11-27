using System.Collections.Generic;
using UnityEngine;

namespace FoggyWoods.Inventories.UI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private SlotDisplay _slotPrefab;
        [SerializeField] private RectTransform _content;

        private IInventory _inventory;
        private Dictionary<string, SlotDisplay> _slotByID = new();

        public void Initialize(IInventory inventory)
        {
            _inventory = inventory;

            _inventory.Changed += OnInventoryChanged;
            _inventory.SlotAdded += OnSlotAdded;
            _inventory.SlotRemoved += OnSlotRemoved;

            foreach (ISlot slot in _inventory.Slots)
                OnSlotAdded(slot);
        }

        private void OnDestroy()
        {
            _inventory.Changed -= OnInventoryChanged;
            _inventory.SlotAdded -= OnSlotAdded;
            _inventory.SlotRemoved -= OnSlotRemoved;
        }

        private void OnInventoryChanged(ItemChangedData data) { }

        private void OnSlotAdded(ISlot slot)
        {
            _slotByID.Add(slot.Item.ID, CreateSlotDisplay(slot));
        }

        private void OnSlotRemoved(ISlot slot)
        {
            Destroy(_slotByID[slot.Item.ID]);
            _slotByID.Remove(slot.Item.ID);
        }

        private SlotDisplay CreateSlotDisplay(ISlot slot)
        {
            SlotDisplay slotDisplay = Instantiate(_slotPrefab, _content);
            slotDisplay.Initialize(slot);
            return slotDisplay;
        }
    }
}