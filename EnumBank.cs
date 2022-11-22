using System;
using AutoModApi.Attributes.Documentation;
using static OpenRpg.EnumBank;

namespace OpenRpg;

public static class EnumBank
{
    [EnumDoc]
    public enum AttCost
    {
        Spell,
        Stamina,
        Mana
    }
    
    [EnumDoc]
    public enum LootType
    {
        Necklace,
        Gauntlet,
        Ring,
        Potion,
        Weapon
    }

    [EnumDoc]
    public enum LootTable
    {
        Boss,
        Elite,
        Monster,
        Chest,
        Random,
        All
    }

    [EnumDoc]
    public enum LootRarity
    {
        Basic,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [EnumDoc]
    public enum Binding
    {
        Healthy,
        Coins,
        Potions,
        Keys,
        Bombs,
        Enemy
    }

    [EnumDoc]
    public enum Consumables
    {
        Key,
        Bomb,
        Potion,
        Coin,
    }

    [EnumDoc]
    public enum EnemyType
    {
        Boss,
        Elite,
        Monster
    }
}

public static class EnumMethods
{
    public static ConsoleColor ToColor(this LootRarity r)
    {
        return r switch
        {
            LootRarity.Common => ConsoleColor.White,
            LootRarity.Uncommon => ConsoleColor.Green,
            LootRarity.Rare => ConsoleColor.DarkBlue,
            LootRarity.Epic => ConsoleColor.DarkCyan,
            LootRarity.Legendary => ConsoleColor.Yellow,
            _ => ConsoleColor.Gray
        };
    }

    public static ConsoleColor ToColor(this EnemyType type)
    {
        return type switch
        {
            EnemyType.Boss => ConsoleColor.Red,
            EnemyType.Elite => ConsoleColor.DarkYellow,
            EnemyType.Monster => ConsoleColor.Magenta,
            _ => ConsoleColor.Gray
        };
    }
}