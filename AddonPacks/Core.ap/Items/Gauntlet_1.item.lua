function Init(item, lvl)
    item.name = "Angelic Gauntlet"
    item.desc = "+5 Potions every floor"
    item.Loot("gauntlet", "chest", "uncommon")
end

function OnNewFloor(player)
    player.AddToInv("potion", 5)
end