using InventorySystem;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public Inventory _inventory = new Inventory();

    [ContextMenu("Clear")]
    private void ClearSave()
    {
        ES3.DeleteFile();
    }

    [ContextMenu("Save")]
    private void Save()
    {
        ES3.Save("sk", _inventory);
    }

    [ContextMenu("Load")]
    private void Load()
    {
        _inventory = ES3.Load("sk", new Inventory());
    }
}