function Init(item, lvl)
    item.name = "Healthy Necklace"
    item.desc = "+50% more Health from Healing"
    item.Loot("necklace", "chest", "uncommon")
end

function OnPlayerHeal(player, heal) 
    return heal * 1.5
end