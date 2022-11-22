using System;
using System.Collections.Generic;
using System.Linq;
using AutoModApi;
using AutoModApi.Attributes.Api;
using AutoModApi.Attributes.Documentation;

namespace OpenRpg.Scriptables;

public class Room : Scriptable
{
    [Document("Possible types of enemies")]
    public List<Enemy> PossibleEnemies { get; private set; } = new();

    [Document("How many chests are in the room")]
    public Range Chests { get; set; } = 1..3;

    private float _enemyLevel;

    public void Init(float enemyLevel)
    {
        _enemyLevel = enemyLevel;
        Execute("Init", new InitArgs(this));
    }

    public void AddEnemyPossibility(string enemyName)
    {
        if (!Api.DoesTypeExist<Enemy>(enemyName)) return;
        var enemy = Api.CreateType<Enemy>(enemyName);
        enemy.Init(_enemyLevel);
        PossibleEnemies.Add(enemy);
    }

    public void RemoveEnemyPossibility(string enemyName)
    {
        if (!Api.DoesTypeExist<Enemy>(enemyName)) return;
        var enemy = Api.CreateType<Enemy>(enemyName);
        enemy.Init(_enemyLevel);
        PossibleEnemies.RemoveAll(e => e == enemy);
    }

    public void AddEnemiesByTag(string tag)
    {
        if (!Enemy.EnemyTags.ContainsKey(tag.ToLower())) return;
        Enemy.EnemyTags[tag.ToLower()].ForEach(AddEnemyPossibility);
    }

    public void RemoveEnemiesByTag(string tag) => PossibleEnemies.RemoveAll(e => e.Tags.Contains(tag));

    public void AddEnemiesByType(string type)
    {
        if (!Enemy.EnemyTypes.ContainsKey(type.ToEnum<EnumBank.EnemyType>())) return;
        Enemy.EnemyTypes[type.ToEnum<EnumBank.EnemyType>()].ForEach(AddEnemyPossibility);
    }

    public void RemoveEnemiesByType(string type)
    {
        var enm = type.ToEnum<EnumBank.EnemyType>();
        PossibleEnemies.RemoveAll(e => e.EnemyType == enm);
    }

    [ApiArgument("Init")] public record InitArgs(Room This);

    public override string GetData()
    {
        return $"""
        Possible enemy spawns:
            {(PossibleEnemies.Any() ? string.Join("\n    ", PossibleEnemies.Select(e => e.Name)) : "N/A")}        
        
        From [#darkyellow][{Chests.Start}][#r] to [#darkyellow][{Chests.End}][#r] chests will spawn
        """;
    }
}