function Init(item, lvl)
    item.name = "Skillful Ring"
    item.desc = "A 50% chance to 5x Xp"
    item.Loot("ring", "all", "legendary")
end 

function OnXpEarn(player, amt)
    if math.random(0, 1) == 1 then
        return amt * 5
    end
    return amt
end 