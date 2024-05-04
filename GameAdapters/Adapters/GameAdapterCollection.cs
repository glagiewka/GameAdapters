namespace GameAdapters.Adapters;

public class GameAdapterCollection {
    private List<IGameAdapter> _adapters = new List<IGameAdapter>();

    public void AddAdapter(IGameAdapter adapter) {
        if (IsAdapterAlreadyRegistered(adapter)) {
            throw new InvalidOperationException("An adapter of this type is already registered");
        }

        _adapters.Add(adapter);
    }
    
    public IGameAdapter GetAdapter(string name) {
        var adapter = _adapters.FirstOrDefault(item => item.Name.Equals(name));

        if (adapter is null) {
            throw new InvalidOperationException($"Adapter with name {name} is not registered");
        }

        return adapter;
    }

    private bool IsAdapterAlreadyRegistered(IGameAdapter adapter) {
        return _adapters.Select(item => item.GetType()).ToHashSet().Contains(adapter.GetType());
    }
}
