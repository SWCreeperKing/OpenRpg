function Init(item)
    item.name = "Experienced Necklace"
    item.desc = "+50% more Xp"
    item.Loot("necklace", "boss", "uncommon")
end

function OnXpEarn(player, amt)  
    return amt * 1.5
end