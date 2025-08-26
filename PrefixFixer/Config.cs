using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace PrefixFixer;

public class Config: BasePluginConfig
{
    [JsonPropertyName("Prefix")]
    public string Prefix { get; init; } = " \x04[PrefixFixer]\x01";

    [JsonPropertyName("PrefixsToFix")]
    public List<string> PrefixsToFix { get; init; } = ["[CSS]"];
}