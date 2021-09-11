## Table of Contents

----

- [Archetype](#archetype)
- [Difficulty](#difficulty)
- [Item](#item)
- [Player](#player)
- [Enums](#Enums)

## Notes

----

### Types and Returns

Methods will have a (R: ) to specify a return type IF they do return anything, if a method does not have a (R: ) then
that means it does not return anything. Fields will have a (T: ) to specify the type that the field is

- Types
    - N is short for a Number type
    - S is short for a String type

### Misc

- Methods marked with an (E) is an event method, it will carry a variable marked with a (C) to other versions of the
  same method, by default they return (C)
- Methods with marked with a (L) are not overrideable in the lua files and only to be accessed from the object its
  attached to
- Enums should be treated as strings, enum returns should be strings (case insensitive) and will be auto converted
- The .lua in the file extensions are optional, it is mostly there to help with text editors to pick up the syntax

### File Extensions

- [.arc.lua](#archetype)
- .att.lua
- [.diff.lua](#difficulty)
- [.item.lua](#item)
- .status.lua

## Archetype

.arc.lua

----

#### Fields

- (T:S) className
    - Name of the player's class
- (T:S) weaponName
    - Name of the player's weapon

#### Methods

- Init([Archetype](#archetype))
    - Archetype initialization

## Difficulty

.diff.lua

----

#### Fields

- (T:S) name
    - Name of the difficulty
- (T:N) baseModifier
    - Base difficulty modifier

#### Methods

- (R:N) FloorModifier((T:N) floor)
    - Returns the modifier of the floor number the player is on

## Enemy

----

## Item

.item.lua

----

#### Fields

- (T:S) name
    - Name of the item
- (T:S) desc
    - Description of the item
- (T:S) lore
    - Lore of the item
- (T:N) protection
    - Amount of protection the item gives
- (T:N) itemLevel
    - Level of the item
- (T:N) damage
    - Amount of damage the item does (all items equipped will be tallied)

#### Methods

- Init([Item](#item), (Number) itemLevel)
    - Item initialization
- (L) Loot([LType, LTable, LRarity](#enums))
    - Sets the loot information for the item
- OnNewFloor([Player](#player))
    - For when the player enters a new floor
- (E) (R:N) OnConsPickup([Player](#player), [Cons](#enums), (C) (T:N) amt)
    - Amt is the amount of consumables
- (E) (R:N) OnDealDamage([Player](#player), [Enemy](#enemy), (C) (T:N) dmg)
    - Dmg is the amount of damage to deal
- (E) (R:N) OnPlayerDamaged([Player](#player), [Enemy](#enemy), (C) (T:N) dmg)
    - Dmg is the amount of damage received
- (E) (R:N) OnPlayerHeal([Player](#player), (C) (T:N) heal)
    - Heal is the amount of hp to heal
- (E) (R:N) OnPotionUse([Player](#player), (C) (T:N) amt)
    - Amt is the amount of potions used
- (E) (R:N) OnXpEarn([Player](#player), (C) (T:N) amt)
    - Amt is the amount of xp to gain

## Player

----

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