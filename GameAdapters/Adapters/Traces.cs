namespace GameAdapters.Adapters;

public record Traces
{
    public double Throttle { get; init; }
    public double Brake { get; init; }
    public double Clutch { get; init; }
    public double Steering { get; init; }
}