function Init(item, lvl)
    item.name = "Demonic Gauntlet"
    item.desc = "+5 Bombs and Keys every floor"
    item.Loot("gauntlet", "elite", "epic")
end

function OnNewFloor(player)
    player.AddToInv("bomb", 5)
    player.AddToInv("key", 5)
end