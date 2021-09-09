function Init(item)
    item.name = "Dirt [w]"
    item.desc = "This is a basic, dirty weapon you found. It's somehow able to stay in one piece"
    item.Loot("weapon", "monster", "basic")
end

function Damage(item)
    return item.itemLevel * 4
end