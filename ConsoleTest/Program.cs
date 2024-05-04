using GameAdapters.Adapters;
using GameAdapters.Adapters.AssettoCorsa;

var statusManager = new GameStatusAdapterManager();
statusManager.AddAdapter(new AssettoCorsaStatusAdapter());

var adapterCollection = new GameAdapterCollection();
adapterCollection.AddAdapter(new AssettoCorsaAdapter());

CancellationTokenSource? runningGameAdapterCancellationTokenSource = null;

statusManager.Activated += (sender, args) => { 
    Console.WriteLine($"{args.Name} connected");
    runningGameAdapterCancellationTokenSource = new CancellationTokenSource();
    var adapter = adapterCollection.GetAdapter(args.Name);
    adapter.TracesChanged += (sender, args) => Console.WriteLine(args.ToString());
    adapter.Run(runningGameAdapterCancellationTokenSource.Token);
};

statusManager.Deactivated += (sender, args) => {
    Console.WriteLine($"{args.Name} disconnected");
    runningGameAdapterCancellationTokenSource?.Cancel();
};

Console.WriteLine("Listening for games");

var statusManagerCancellationToken = new CancellationToken();
await statusManager.Run(statusManagerCancellationToken);

