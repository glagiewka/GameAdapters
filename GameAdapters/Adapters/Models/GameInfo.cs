namespace GameAdapters.Adapters.Models;

public record GameInfo
{
    public required string Name { get; init; } = string.Empty;
    public required string Version { get; init; } = string.Empty;
    public required long Timestamp { get; init; }
}