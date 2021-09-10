function Init(item, lvl)
    item.name = "Plentiful Ring"
    item.desc = "A 50% chance to 5x Gold Coins"
    item.Loot("ring", "boss", "epic")
end 

function OnConsPickup(player, cons, amt)
    if cons == "coin" or math.random(0, 1) == 1 then
        return anmt * 5
    end
    return amt
end 