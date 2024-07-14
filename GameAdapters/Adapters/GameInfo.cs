namespace GameAdapters.Adapters;

public record GameInfo
{
    public string Name { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
}