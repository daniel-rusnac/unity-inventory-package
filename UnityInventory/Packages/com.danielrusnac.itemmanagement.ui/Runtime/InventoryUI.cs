using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ItemManagement.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private int _amount = 1;
        [SerializeField] private ItemDefinition _item;
        [SerializeField] private InventoryComponent _inventory;
        [SerializeField] private UIDocument _uiDocument;

        private Label _totalSlotsLabel;
        private Label _totalItemsLabel;
        private VisualElement _root;

        private void Awake()
        {
            _inventory.ItemChanged += OnInventoryChanged;
        }

        private void OnDestroy()
        {
            _inventory.ItemChanged -= OnInventoryChanged;
        }

        private void Start()
        {
            _root = _uiDocument.rootVisualElement;
            
            _totalSlotsLabel = _root.Q<Label>("total-slot-amount");
            _totalItemsLabel = _root.Q<Label>("total-item-amount");

            _root.Q<SliderInt>("amount").RegisterValueChangedCallback(OnAmountChanged);
            _root.Q<Button>("button-add").clicked += OnAddClicked;
        }

        private void OnInventoryChanged(ItemChangedData data)
        {
            _totalSlotsLabel.text = $"Slots: {GetTotalSlots()}";
            _totalItemsLabel.text = $"Items: {GetTotalItems()}";
        }

        private int GetTotalSlots()
        {
            return _inventory.AllSlots.Count();
        }

        private int GetTotalItems()
        {
            return _inventory.AllSlots.Sum(slot => slot.Amount);
        }

        private void OnAddClicked()
        {
            AddItem();
        }

        [ContextMenu(nameof(AddItem))]
        private void AddItem()
        {
            if (!_inventory.ContainsSlot(_item.ID))
                _inventory.AddSlot(_item.ID, new Slot(new Item(_item)));

            _inventory.GetSlot(_item.ID).Amount += _amount;
        }

        private void OnAmountChanged(ChangeEvent<int> evt)
        {
            _amount = evt.newValue;
        }
    }
}