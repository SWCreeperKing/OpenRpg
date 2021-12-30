function Init(diff)
    diff.name = "Easy"
    diff.baseModifier = .5
    diff.desc = "A calm difficulty"
end

function FloorModifier(floor)
    return floor/2
end