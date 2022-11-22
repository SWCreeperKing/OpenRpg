using AutoModApi.Attributes.Api;
using AutoModApi.Attributes.Documentation;

namespace OpenRpg.Scriptables;

public class Difficulty : Scriptable
{
    [Document("Difficulty name")] public string Name { get; set; } = "Unknown Difficulty";
    [Document("Difficulty description")] public string Desc { get; set; } = "No Description Provided";

    [Document("Difficulty base difficulty modifier")]
    public double BaseModifier { get; set; } = 1f;

    [Document("Difficulty initialization method")]
    public void Init() => Execute("Init", new InitArgs(this));

    [Document("Returns the modifier of the floor number the player is on")]
    public double Modifier(long floor) => ExecuteAndReturn("FloorModifier", new ModifierArgs(floor), floor).Result;

    [ApiArgument("Init")] public record InitArgs(Difficulty This);

    [ApiArgument("Modifier")] public record ModifierArgs(long Floor);

    public override string GetData()
    {
        return $"""
        Difficulty Name: {Name}
        Modifier: {BaseModifier}
        Description:
        {Desc}
        """;
    }
}