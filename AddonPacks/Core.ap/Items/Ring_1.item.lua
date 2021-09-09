function Init(item)  
    item.name = "Lovely Ring"
    item.desc = "A 50% chance to 5x health potions"
    item.Loot("ring", "all", "epic")
end

function OnConsPickup(player, cons, amt)
    if cons == "potion" and math.random(0, 1) == 1 then
       return amt * 5
    end
    return amt
end

function OnPotionUse(player, amt)
    player.Heal(15)
    return amt
end