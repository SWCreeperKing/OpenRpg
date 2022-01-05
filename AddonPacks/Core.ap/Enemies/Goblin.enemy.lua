function Init(enemy, lvl)
    enemy.name = "[#green]Goblin[#r]"
    enemy.defense = -1
    enemy.attack = lvl
    enemy.maxHp = 15 * lvl
    enemy.speed = 2
    enemy.speedRegen = 1.5
    enemy.desc = "A small, but very fast creature"
end 