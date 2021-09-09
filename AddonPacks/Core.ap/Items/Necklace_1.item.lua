function Init(item)
    item.name = "Healthy Necklace"
    item.desc = "+50% more Health from Health Potions"
    item.Loot("necklace", "chest", "uncommon")
end

function OnPlayerHeal(player, heal) 
    return heal * 1.5
end