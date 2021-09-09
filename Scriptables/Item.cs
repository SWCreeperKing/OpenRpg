using System;
using ReUndefinedRpg;
using static ReUndefinedRpg.EnumBank;

namespace OpenRpg
{
    [Index("item", "Item")]
    public class Item : LuaLoader
    {
        public static Random _r = new();

        public string name = "Unknown Item";
        public string desc = "No Description Given";
        public string lore = "Item has no Lore entry";
        public int protection = 1;
        public int itemLevel = 1;
        public int damage = 0;
        public LootType lootType;
        public LootTable lootTable;
        public LootRarity lootRarity;

        private enum Methods
        {
            OnPlayerDamaged,
            OnPlayerHeal,
            OnPotionUse,
            OnConsPickup,
            OnXpEarn,
            OnDealDamage,
            OnNewFloor,
            Init,
        }

        public Item(string rawLua) : base(rawLua)
        {
        }

        public override Enum[] GetMethods() => Values<Methods>();

        public void Init(int itemLevel) => Call(Methods.Init, this, (this.itemLevel = itemLevel));
        public double OnPlayerHeal(Player player, double heal) => Call(Methods.OnPlayerHeal, heal, player);
        public double OnPotionUse(Player player, double amt) => Call(Methods.OnPotionUse, amt, player);
        public double OnXpEarn(Player player, double amt) => Call(Methods.OnXpEarn, amt, player);
        public void OnNewFloor(Player player) => Call(Methods.OnNewFloor, player);

        public double OnPlayerDamaged(Player player, Enemy enemy, double dmg) =>
            Call(Methods.OnPlayerDamaged, dmg, player, enemy);

        public double OnConsumablePickup(Player player, Consumables cons, double amt) =>
            Call(Methods.OnConsPickup, amt, player, cons.ToString().ToLower());

        public double OnDealDamage(Player player, Enemy enemy, double dmg) =>
            Call(Methods.OnDealDamage, dmg, player, enemy);

        public void Loot(string type, string table, string rarity) =>
            (lootType, lootTable, lootRarity) = (type.ToEnum<LootType>(), table.ToEnum<LootTable>(),
                rarity.ToEnum<LootRarity>());

        public override string ToString() => $"[#{lootRarity.ToColor()}]{name}[#r]";

        public Item CopyWithNewLevel(int itemLevel)
        {
            var copy = this.Clone(rawLua);
            copy.itemLevel = itemLevel;
            return copy;
        }
    }
}