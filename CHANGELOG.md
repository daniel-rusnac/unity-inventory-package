# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)

## [2.0.2] - 2022-03-09

### Changed
- Static id is now assigned by default, even when an item was duplicated
- No longer creating an instance from the added item (if it's an instance already)

### Fixed
- A error when saving an empty inventory 

## [2.0.1] - 2022-02-28

### Changed
- Move the icon property to the base ItemSO

### Fixed
- Fixed the default load throwing errors when loading before at least one save was made
- Fixed empty save throwing errors on deserialization 

## [2.0.0] - 2022-02-27

### Added
- Dynamic Items
- A better content view for inventory inspector

### Changed
- Reworked the whole inventory system.
- Improved inventory serialization
- The inventory is now saved as a binary file.

### Removed
- Old inventory
- Old item

## [1.5.1] - 2022-02-23

### Fixed
- Empty inventory deserialization

## [1.5.0] - 2022-02-19

### Added
- Item database runtime implementations

### Changed
- InventoryUtility now must be initialized with a InventoryDatabase implementation
- No longer bound to place Items in the resources folder
- Inventory database is always initialized before the default time

### Removed
- Removed obsolete code

## [1.4.4] - 2022-02-18

### Changed
- Now all items must be under the "Resources/Items" path

## [1.4.3] - 2022-02-18

### Added
- Register/unregister methods
- A method that returns the total amount of the inventory

### Fixed
- The delta of a OnChanged event always being positive

### Deprecated
- Old OnChanged event are now obsolete

## [1.4.2] - 2022-02-17

### Changed
- Rename 'max' to 'limit'
- Using a 'long' instead of a 'int' for amount and limit

### Fixed
- The amount resetting to 0 when the max value of the number type was reached

## [1.4.1] - 2022-02-14

### Fixed
- Fixed editor dependencies

## [1.4.0] - 2022-02-14

### Added
- The inventory editor is now persistent through hot reloads and unity sessions
- Default save/load methods using PlayerPrefs
- The ability to retrieve from inventory all items of a certain type ```Inventory.GetItems<ItemType>()```
- A method to set the item amount to a value
- Added more package info to be visible from the package manager

### Changed
- Item database is now always initialized
- The inventory is now a scriptable object
- The inventory now works with items directly
- Inventory now serializes into a string

### Removed
- No more methods on the inventory that receive an ID

## [1.3.2] - 2022-02-01

### Fixed

- ItemSO Editor dependency

## [1.3.1] - 2022-01-31

### Added

- A simple ID generator for items
- Summaries and a README with instructions

### Fixed

- Fixed the error from GUI Style creation in `ItemSOEditor`

## [1.3.0] - 2022-01-31

### Added 
- A method to get a list of all items/ids in the inventory

### Changed
- Changed item id type from 'byte' to 'int'

## [1.2.0] - 2022-01-26

### Added
- Inventory property drawer, with the ability to add and remove items
- InventorySO
- Item icon labels

### Changed
- Removed the [FormerlySerializedAs] attribute from item parameters

### Fixed
- Fixed the SetMax on inventory slot not setting the actual value when the slot was previously modified
- Fixed slot value not being clamped to the max value

## [1.1.0] - 2022-01-21

### Added
- Inventory serialization
- A optional 'disabled' icon variant to the item
- Methods to register for inventory updates
- Item variant overloads for the inventory
- Inventory extensions

### Changed
- Items are now loaded at every startup

### Removed
- 'OnChanged' event from the inventory

## [1.0.1] - 2022-01-10

### Fixed
- Updated .meta GUID that caused SOArchitecture incompatibility

## [1.0.0] - 2022-01-10

### Added
- Basic inventory logic
- Licence
- Changelog

[Unreleased]: https://github.com/danielrusnac/unity-inventory-package
[2.0.2]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v2.0.2
[2.0.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v2.0.1
[2.0.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v2.0.0
[1.5.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.5.1
[1.5.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.5.0
[1.4.4]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.4.4
[1.4.3]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.4.3
[1.4.2]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.4.2
[1.4.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.4.1
[1.4.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.4.0
[1.3.2]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.2
[1.3.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.1
[1.3.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.0
[1.2.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.2.0
[1.1.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.1.0
[1.0.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.0.1
[1.0.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.0.0
