function Init(item)
    item.name = "Strong Necklace"
    item.desc = "+25% more Attack Damage"
    item.Loot("necklace", "monster", "uncommon")
end 

function OnDealDamage(player, enemy, dmg)
    return dmg * 1.25
end