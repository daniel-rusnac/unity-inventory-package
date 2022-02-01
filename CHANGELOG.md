# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html)

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
[1.0.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.0.0
[1.0.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.0.1
[1.1.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.1.0
[1.2.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.2.0
[1.3.0]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.0
[1.3.1]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.1
[1.3.2]: https://github.com/danielrusnac/unity-inventory-package/releases/tag/v1.3.2

