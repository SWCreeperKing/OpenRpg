function Init(item, lvl)
    item.name = "Dirt [w]"
    item.desc = "This is a basic, dirty weapon you found. It's somehow able to stay in one piece"
    item.Loot("weapon", "monster", "basic")
    item.damage = lvl * 4
end