using System;
using TMPro;
using UnityEngine;

namespace FoggyWoods.Inventories.UI
{
    public class SlotDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _amount;

        private ISlot _slot;

        private void OnDestroy()
        {
            _slot.Changed -= OnChanged;
        }

        public void Initialize(ISlot slot)
        {
            _slot = slot;
            _slot.Changed += OnChanged;
            
            _name.SetText(slot.Item.ID);
            RefreshAmount();
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void OnChanged(ItemChangedData data)
        {
            RefreshAmount();
        }

        private void RefreshAmount()
        {
            _amount.SetText(_slot.Amount.ToString());
        }
    }
}