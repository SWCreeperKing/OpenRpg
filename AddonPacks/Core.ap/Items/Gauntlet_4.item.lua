function Init(item)
    item.name = "Vengeful Gauntlet"
    item.desc = "+(3 * floor level) damage"
    item.Loot("gauntlet", "random", "rare")
end

function OnDealDamage(player, enemy, dmg)
    return dmg + 3 * player.map.floor
end 