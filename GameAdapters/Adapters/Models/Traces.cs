namespace GameAdapters.Adapters.Models;

public record Traces
{
    public required double Throttle { get; init; }
    public required double Brake { get; init; }
    public required double Clutch { get; init; }
    public required double Steering { get; init; }

    public required long Timestamp { get; init; }
}