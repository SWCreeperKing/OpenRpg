function Init(item, lvl)
    item.name = "Rich Gauntlet"
    item.desc = "+25% of your current gold every floor"
    item.Loot("gauntlet", "elite", "legendary")
end

function OnNewFloor(player)
    player.AddToInv(player.GetFromInv("coin") * 1.25)
end