using AutoModApi.Attributes.Api;
using AutoModApi.Attributes.Documentation;

namespace OpenRpg.Scriptables;

public class Archetype : Scriptable
{
    [Document("Archetype name")] public string ClassName { get; set; } = "Unknown";
    [Document("Archetype weapon name")] public string WeaponName { get; set; } = "Stick";
    [Document("Archetype description")] public string Desc { get; set; } = "No Description Provided";
    [Document("Archetype attack speed")] public float Speed { get; set; } = 1;

    [Document("Archetype regeneration speed")]
    public float SpeedRegen { get; set; }  = 1;

    [Document("Archetype defense")] public int Defense { get; set; } = 1;
    [Document("Archetype attack damage")] public int Attack { get; set; } = 1;
    [Document("Archetype max health")] public int MaxHp { get; set; } = 100;
    [Document("Archetype xp modifier")] public float XpMod { get; set; } = 1f;

    [Document("Archetype initialization method")]
    public void Init() => Execute("Init", new InitArgs(this));

    [ApiArgument("Init")] public record InitArgs(Archetype This);

    public override string GetData()
    {
        return $"""
        Class Name: [{ClassName}]
        Weapon Type: [{WeaponName}]
        Max Hp: [#red]{MaxHp}[#r] | defense: [#gray]{Defense}[#r] | Attack: [#blue]{Attack}[#r]
        Speed: {Speed} (+{SpeedRegen}/other turn)
        Description:
        {Desc}
        """;
    }
}