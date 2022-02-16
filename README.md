# Inventory
An inventory system for unity that supports adding/removing items and saving/loading the content.

## Installation

- Go to *Window/Package Manager*
- Click on *+ Add package from git URL...*
- Paste link for the wanted version.

To get a specific version of the package, go to releases and copy the "Package Link".

## Usage

### Item

All items **MUST** be in the *Resources* folder, so they can be loaded by the Inventory System.
A basic item can be created from *Create/Inventory/Item* and looks like this:

![image](https://user-images.githubusercontent.com/43440044/151854024-6c86c3a6-7dfd-4949-942f-7a2a81c89a64.png)

- *Item Name:* use to display in IU
- *ID:* used by the inventory to store and load items
- *Glyph:* an optional string to use with TextMeshPro to display sprites inside text
- *Icon Normal:* item icon to display in UI
- *Icon Locked:* optional icon to display the item as locked in the UI

To create your own, custom items, inherit from `ItemSO`.
To retrieve an item from the ID use `InventoryUtility.TryGetItem()`.

### Inventory

#### Create
The inventory is a scriptable object and ca be created by *Right Click/Create/Inventory/Inventory*.
If you want to create inventories during runtime, use `CreateInstance()`.

#### Edit
Change the amount of an item by using `Add`, `Remove`, `SetAmount`.
You can also set an upper limit with `SetLimit`. The amount is a `long` clamped between 0 and `long.Max` or `limit` if set.

#### Save
The inventory can be save by calling ```Inventory.Save(saveKey)``` and ```inventory.Load(saveKey)```. If you want to use your custom save system, there are alos methods for serialization into/from a string  ```Inventory.Serialize()``` and ```Inventor.Deserialize()```.
