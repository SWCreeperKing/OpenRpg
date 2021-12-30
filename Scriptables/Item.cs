using System;
using ReUndefinedRpg;
using static ReUndefinedRpg.EnumBank;

namespace OpenRpg
{
    [Index("item", "Item")]
    public class Item : LuaLoader
    {
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

        public static Random _r = new();

        public string name = "Unknown Item";
        public string desc = "No Description Given";
        public string lore = "Item has no Lore entry";
        public double protection = 1;
        public double itemLevel = 1;
        public double damage = 0;
        public LootType lootType;
        public LootTable lootTable;
        public LootRarity lootRarity;

        public Item(string rawLua, string id) : base(rawLua, id)
        {
        }

        public Item Init(int itemLevel)
        {
            Call(Methods.Init, this, this.itemLevel = itemLevel);
            return this;
        }

        public double OnPlayerHeal(Player player, double heal) => Call(Methods.OnPlayerHeal, heal, player);
        public double OnPotionUse(Player player, double amt) => Call(Methods.OnPotionUse, amt, player);
        public double OnXpEarn(Player player, double amt) => Call(Methods.OnXpEarn, amt, player);
        public void OnNewFloor(Player player) => Call(Methods.OnNewFloor, player);

        public double OnPlayerDamaged(Player player, Enemy enemy, double dmg) =>
            Call(Methods.OnPlayerDamaged, dmg, player, enemy);

        public double OnConsumablePickup(Player player, Consumables cons, double amt) =>
            Call(Methods.OnConsPickup, amt, player, cons.Name().ToLower());

        public double OnDealDamage(Player player, Enemy enemy, double dmg) =>
            Call(Methods.OnDealDamage, dmg, player, enemy);

        public void Loot(string type, string table, string rarity) =>
            (lootType, lootTable, lootRarity) = (type.ToEnum<LootType>(), table.ToEnum<LootTable>(),
                rarity.ToEnum<LootRarity>());

        public override Enum[] GetMethods() => Values<Methods>();

        public override string GetData() =>
            $@"Item Name: {name}
Rarity: [#{lootRarity.ToColor()}]{lootRarity}[#r]
Type: {lootType}
Table: {lootTable}
Description: {desc}";

        public override string ToString() => $"[#{lootRarity.ToColor()}]Lv.{itemLevel} {name}[#r]";
    }
}