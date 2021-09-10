function Init(item, lvl)
    item.name = "Ascended Gauntlet"
    item.desc = "+5% Xp needed to level every floor"
    item.Loot("gauntlet", "boss", "legendary")
end

function OnNewFloor(player)
    player.GainXp(player.RequiredXp() * 0.05)
end


