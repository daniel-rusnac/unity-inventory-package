using System;
using InventorySystem;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    [SerializeField] private ItemSO _item;
    [SerializeField] private int _amount = 1;
    [SerializeField] private string _inventoryValue;
    

    public Inventory _inventory = new Inventory();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            _inventory.Add(_item, _amount);
            OnChanged();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            _inventory.Remove(_item, _amount);
            OnChanged();
        }
    }

    private void OnChanged()
    {
        _inventoryValue = _inventory.ToString();
    }

    [ContextMenu("Clear")]
    private void ClearSave()
    {
        ES3.DeleteFile();
    }

    [ContextMenu("Save")]
    private void Save()
    {
        ES3.Save("sk", _inventory);
        OnChanged();

    }

    [ContextMenu("Load")]
    private void Load()
    {
        _inventory = ES3.Load("sk", new Inventory());
        OnChanged();
    }
}