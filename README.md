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

### Inventory

There are two variants to use an inventory:
- Create one directly in code using `new Inventory()`
- Use an SO holder `InventorySO` to store and reference the inventory `InventorySO.GetInventory`

The basic operations with an inventory are `Add`, `Remove` and `SetMax`.
The item amount is clamped between `0` and `MaxValue`, or `int.Max`, if the max value is `-1`.