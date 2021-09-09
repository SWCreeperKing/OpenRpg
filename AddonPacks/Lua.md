## Table of Contents

----

- [ItemClass](#item)
- [Enums](#Enums)

## Notes

----

- Methods with marked with a (L) are not overrideable in the lua files and only to be accessed from the object its attached to
- Enums should be treated as strings, enum returns should be strings (case insensitive) and will be auto converted
- The .lua in the file extensions are optional, it is mostly there to help with text editors to pick up the syntax

## File Extensions

----

- .status.lua
  - defines a lua file to be interpreted as a status effect
- .item.lua
  - defines a lua file to be interpreted as an item
- .item.class.lua
  - defines a lua file to be interpreted as an item class
- .class.lua
  - defines a lua file to be interpreted as a player class
- .att.lua
  - defines a lua file to be interpreted as an attack
- .diff.lua
  - defines a lua file to be interpreted as a difficulty

## Player

----


## Item
.item.lua

----

- Fields 
  - (String) name
    - Name of the item
  - (String) desc
    - Description of the item
  - (String) lore
    - Lore of the item
  - (Int) protection
    - Amount of protection the item gives
  - (Int) itemLevel
    - Level of the item
- Methods
  - (Void) Init([Item](#item), (Int) itemLevel)
    - Item initialization
  - (L) (Void) Loot([LType, LTable, LRarity](#enums))
    - Sets the loot information for the item
  - (Int) OnConsPickup([Player](#player), [Cons](#enums), (Int) amt)
    - Returns the amt of consumables to pickup, amt is chained from event to event

## Enums

----

|`Cons`umables|`L`oot`Type`|`L`oot`Table`|`L`oot`Rarity`|
|:------------|:-----------|:------------|:-------------|
|"key"        |"necklace"  |"boss"       |"basic"       |
|"bomb"       |"gauntlet"  |"elite"      |"common"      |
|"potion"     |"ring"      |"monster"    |"uncommon"    |
|"coin"       |"potoin"    |"random"     |"rare"        |
|             |"weapon"    |"chest"      |"epic"        |
|             |            |"all"        |"legendary"   |