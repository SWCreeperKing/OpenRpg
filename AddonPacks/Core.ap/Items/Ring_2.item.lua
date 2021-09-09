function Init(item)
    item.name = "Reinforced Ring"
    item.desc = "A 50% chance to 5x Bombs and Keys"
    item.Loot("ring", "monster", "epic")
end 

function OnConsPickup(player, cons, amt)
    if (cons == "key" or cons == "bomb") and math.random(0, 1) == 1 then
        return amt * 5
    end
    return amt
end 