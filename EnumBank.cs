using System;
using static ReUndefinedRpg.EnumBank;

namespace ReUndefinedRpg
{
    public static class EnumBank
    {
        public enum AttCost
        {
            Spell,
            Stamina,
            Mana
        }

        public enum LootType
        {
            Necklace,
            Gauntlet,
            Ring,
            Potion,
            Weapon
        }

        public enum LootTable
        {
            Boss,
            Elite,
            Monster,
            Chest,
            Random,
            All
        }

        public enum LootRarity
        {
            Basic,
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }

        public enum Binding
        {
            Healthy,
            Coins,
            Potions,
            Keys,
            Bombs,
            Enemy
        }

        public enum Consumables
        {
            Key,
            Bomb,
            Potion,
            Coin,
        }
    }

    public static class EnumMethods
    {
        public static ConsoleColor ToColor(this LootRarity r) => r switch
        {
            LootRarity.Common => ConsoleColor.White,
            LootRarity.Uncommon => ConsoleColor.Green,
            LootRarity.Rare => ConsoleColor.DarkBlue,
            LootRarity.Epic => ConsoleColor.DarkCyan,
            LootRarity.Legendary => ConsoleColor.Yellow,
            _ => ConsoleColor.Gray
        };
    }
}