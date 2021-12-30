function Init(item, lvl) 
    item.name = "Greedy Necklace"
    item.desc = "2x Gold Coins"
    item.lore = "Ooh! Piece of candy! Ooh! Piece of candy! Ooh! Piece of candy!"
    item.Loot("necklace", "chest", "rare")
end

function OnConsPickup(player, cons, amt)
    if cons == "coin" then
        return amt * 2
    end
    return amt
end


