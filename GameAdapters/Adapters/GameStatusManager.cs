namespace GameAdapters.Adapters;

public class GameStatusManager {
    public event EventHandler<IGameStatusAdapter>? Activated;
    public event EventHandler<IGameStatusAdapter>? Deactivated;
 
    private readonly IList<IGameStatusAdapter> _adapters = new List<IGameStatusAdapter>();

    public GameStatusManager AddAdapter(IGameStatusAdapter adapter) {
        if (IsAlreadyRegistered(adapter)) {
            throw new InvalidOperationException("An adapter of this type is already registerd");
        }

        _adapters.Add(adapter);
        adapter.Activated += (sender, args) => Activated?.Invoke(this, adapter);
        adapter.Deactivated += (sender, args) => Deactivated?.Invoke(this, adapter);
        
        return this;
    }

    public async Task Run(CancellationToken cancellationToken) {
       await Task.WhenAll(_adapters.Select(item => item.Run(cancellationToken))); 
    }

    private bool IsAlreadyRegistered(IGameStatusAdapter adapter) {
        return _adapters.Select(item => item.GetType()).ToHashSet().Contains(adapter.GetType());
    }
}
