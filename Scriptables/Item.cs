using System;
using AutoModApi.Attributes.Api;
using AutoModApi.Attributes.Documentation;
using static OpenRpg.EnumBank;

namespace OpenRpg.Scriptables;

public class Item : Scriptable
{
    public static Random r = new();

    [Document("Item name")] public string Name { get; set; } = "Unknown Item";
    [Document("Item description")] public string Desc { get; set; } = "No Description Given";
    [Document("Item lore")] public string Lore { get; set; } = "Item has no Lore entry";

    [Document("Item's level of protection")]
    public double Protection { get; set; } = 1;

    [Document("Item level")] public double ItemLevel { get; set; } = 1;
    [Document("Item attack damage")] public double Damage { get; set; } = 0;
    [Document("Item type")] public LootType LootType { get; set; }
    [Document("Item drop table")] public LootTable LootTable { get; set; }
    [Document("Item Rarity")] public LootRarity LootRarity { get; set; }

    [Document("Item Initialization Method")]
    public void Init(int itemLevel) => Execute("Init", new InitArgs(this, itemLevel));

    [Document("For when the player heals")]
    public double OnPlayerHeal(Player player, double heal)
    {
        return ExecuteAndReturn("OnPlayerHeal", new HealArgs(player, heal), heal).Result;
    }

    [Document("For when the player uses a potion")]
    public double OnPotionUse(Player player, double amount)
    {
        return ExecuteAndReturn("OnPotionUse", new PotionArgs(player, amount), amount).Result;
    }

    [Document("For when the player receives xp")]
    public double OnXpEarn(Player player, double amount)
    {
        return ExecuteAndReturn("OnXpEarn", new XpArgs(player, amount), amount).Result;
    }

    [Document("For when the player enters a new floor")]
    public void OnNewFloor(Player player) => Execute("OnNewFloor", new FloorArgs(player));

    [Document("For when the player gets damaged")]
    public double OnPlayerDamaged(Player player, Enemy enemy, double dmg)
    {
        return ExecuteAndReturn("OnPlayerDamaged", new DamagedArgs(player, enemy, dmg), dmg).Result;
    }

    [Api("OnConsPickup"), Document("For when the player picks up a consumable")]
    public double OnConsumablePickup(Player player, Consumables cons, double amount)
    {
        return ExecuteAndReturn("OnConsPickup", new ConsumableArgs(player, $"{cons}".ToLower(), amount), amount).Result;
    }

    [Document("For when the player deals damage")]
    public double OnDealDamage(Player player, Enemy enemy, double dmg)
    {
        return ExecuteAndReturn("OnDealDamage", new DamagedArgs(player, enemy, dmg), dmg).Result;
    }

    [Document("Sets the loot information for the item")]
    public void Loot(string type, string table, string rarity)
    {
        (LootType, LootTable, LootRarity) =
            (type.ToEnum<LootType>(), table.ToEnum<LootTable>(), rarity.ToEnum<LootRarity>());
    }

    [ApiArgument("Init")] public record InitArgs(Item This, int ItemLevel);

    [ApiArgument("OnPlayerHeal")] public record HealArgs(Player Player, double Heal);

    [ApiArgument("OnPotionUse")] public record PotionArgs(Player Player, double Amount);

    [ApiArgument("OnXpEarn")] public record XpArgs(Player Player, double Amount);

    [ApiArgument("OnNewFloor")] public record FloorArgs(Player Player);

    [ApiArgument("OnPlayerDamaged", "OnDealDamage")]
    public record DamagedArgs(Player Player, Enemy Enemy, double Damage);

    [ApiArgument("OnConsPickup")] public record ConsumableArgs(Player Player, string Cons, double Amount);

    public override string GetData()
    {
        return $"""
        Item Name: {Name}
        Rarity: [#{LootRarity.ToColor()}]{LootRarity}[#r]
        Type: {LootType}
        Table: {LootTable}
        Description: 
        {Desc}
        """;
    }
}