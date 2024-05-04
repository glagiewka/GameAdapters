namespace GameAdapters.Adapters;

public interface IGameAdapter {
    event EventHandler<Traces> TracesChanged;
    string Name { get; }
    Task Run(CancellationToken cancellationToken);
}

