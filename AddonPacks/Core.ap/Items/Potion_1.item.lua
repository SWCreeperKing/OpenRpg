function Init(item, lvl)
    item.name = "Beginner Potion"
    item.desc = "The starting potion type, heal for 15 hp"
    item.Loot("potion", "random", "basic")
end 

function OnPotionUse(player, amt)  
    player.Heal(15)
    return amt
end