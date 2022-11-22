using System.Collections.Generic;
using AutoModApi.Attributes.Api;
using AutoModApi.Attributes.Documentation;
using static OpenRpg.EnumBank;

namespace OpenRpg.Scriptables;

public class Enemy : Scriptable
{
    public static Dictionary<string, List<string>> EnemyTags = new();
    public static Dictionary<EnemyType, List<string>> EnemyTypes = new();

    [Document("Enemy name")] public string Name { get; set; } = "Unknown Enemy";
    [Document("Enemy description")] public string Desc { get; set; } = "No Description Provided";
    [Document("Enemy tags")] public List<string> Tags { get; } = new();
    [Document("Enemy attack speed")] public float Speed { get; set; } = 1;
    [Document("Enemy regeneration speed")] public float SpeedRegen { get; set; } = 1;
    [Document("Enemy defense")] public float Defense { get; set; } = 1;
    [Document("Enemy attack damage")] public float Attack { get; set; } = 1;
    [Document("Enemy max health")] public float MaxHp { get; set; } = 100;
    [Document("Enemy type")] public EnemyType EnemyType { get; set; } = EnemyType.Monster; 

    [Document("Enemy's current hp (set in Init automatically)")]
    public float Hp { get; set; }

    [Document("Enemy Initialization method")]
    public void Init(float enemyLevel)
    {
        Execute("Init", new InitArgs(this, enemyLevel));
        Hp = MaxHp;
    }

    public void SetEnemyType(string type) => EnemyType = type.ToEnum<EnemyType>();
    public void AddTags(params string[] tags) => Tags.AddRange(tags);
    
    [ApiArgument("Init")] public record InitArgs(Enemy This, float EnemyLevel);

    public override string GetData()
    {
        return $"""
        [#{EnemyType.ToColor()}]{Name} ({EnemyType})[#r]
        Tags: [#darkyellow][[#darkgray]{string.Join("[#r], [#darkgray]", Tags)}[#r]][#r]
        Max Hp: [#red]{MaxHp}[#r] | defense: [#gray]{Defense}[#r] | Attack: [#blue]{Attack}[#r]
        Speed: {Speed} (+{SpeedRegen}/other turn)
        Description: 
        {Desc}
        """;
    }
}