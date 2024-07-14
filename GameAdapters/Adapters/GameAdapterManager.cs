using GameAdapters.Adapters.AssettoCorsa;
using GameAdapters.Adapters.Models;

namespace GameAdapters.Adapters;

public class GameAdapterManager
{
    private readonly Dictionary<string, CancellationTokenSource> _gameCancellationTokenSources = [];
    private readonly GameStatusAdapterManager _statusManager;
    private CancellationTokenSource? _statusManagerCancellationTokenSource;

    public GameAdapterManager()
    {
        _statusManager = new GameStatusAdapterManager();
        _statusManager.AddAdapter(new AssettoCorsaStatusAdapter());

        var adapterCollection = new GameAdapterCollection();
        adapterCollection.AddAdapter(new AssettoCorsaAdapter());

        _statusManager.Activated += (_, args) =>
        {
            GameStarted?.Invoke(null, args.GetGameInfo());

            var tokenSource = new CancellationTokenSource();
            _gameCancellationTokenSources.Add(args.Name, tokenSource);

            var adapter = adapterCollection.GetAdapter(args.Name);
            adapter.TracesChanged += TracesChanged;
            adapter.Run(tokenSource.Token);
        };

        _statusManager.Deactivated += (_, args) =>
        {
            GameEnded?.Invoke(null, args.Name);

            _gameCancellationTokenSources.GetValueOrDefault(args.Name)?.CancelAsync();
        };
    }

    public event EventHandler<Traces>? TracesChanged;
    public event EventHandler<GameInfo>? GameStarted;
    public event EventHandler<string>? GameEnded;

    public async Task Run()
    {
        _statusManagerCancellationTokenSource = new CancellationTokenSource();
        await _statusManager.Run(_statusManagerCancellationTokenSource.Token);
    }

    public async Task Stop()
    {
        foreach (var (_, value) in _gameCancellationTokenSources) await value.CancelAsync();
    }
}