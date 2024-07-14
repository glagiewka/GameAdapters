namespace GameAdapters.Adapters;

/// <summary>
///     Monitors if a game status adapter has detected a running game
/// </summary>
public class GameStatusAdapterManager
{
    private readonly IList<IGameStatusAdapter> _adapters = [];
    public event EventHandler<IGameStatusAdapter>? Activated;
    public event EventHandler<IGameStatusAdapter>? Deactivated;

    public GameStatusAdapterManager AddAdapter(IGameStatusAdapter adapter)
    {
        if (IsAdapterAlreadyRegistered(adapter))
        {
            throw new InvalidOperationException("An adapter of this type is already registered");
        }

        _adapters.Add(adapter);
        adapter.Activated += (sender, args) => Activated?.Invoke(this, adapter);
        adapter.Deactivated += (sender, args) => Deactivated?.Invoke(this, adapter);

        return this;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_adapters.Select(item => item.Run(cancellationToken)));
    }

    private bool IsAdapterAlreadyRegistered(IGameStatusAdapter adapter)
    {
        return _adapters.Select(item => item.GetType()).ToHashSet().Contains(adapter.GetType());
    }
}