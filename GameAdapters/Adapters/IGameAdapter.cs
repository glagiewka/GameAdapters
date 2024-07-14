using GameAdapters.Adapters.Models;

namespace GameAdapters.Adapters;

public interface IGameAdapter
{
    string Name { get; }
    event EventHandler<Traces> TracesChanged;
    Task Run(CancellationToken cancellationToken);
}