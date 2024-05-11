using GameAdapters.Adapters;
using GameAdapters.Adapters.AssettoCorsa;

var statusManager = new GameStatusAdapterManager();
statusManager.AddAdapter(new AssettoCorsaStatusAdapter());

var adapterCollection = new GameAdapterCollection();
adapterCollection.AddAdapter(new AssettoCorsaAdapter());

CancellationTokenSource? runningGameAdapterCancellationTokenSource = null;

statusManager.Activated += (sender, args) => { 
    runningGameAdapterCancellationTokenSource = new CancellationTokenSource();
    var adapter = adapterCollection.GetAdapter(args.Name);
    adapter.TracesChanged += (sender, args) => PrintTraces(adapter.Name, args); 
    adapter.Run(runningGameAdapterCancellationTokenSource.Token);
};

statusManager.Deactivated += (sender, args) => {
    Console.Clear();
    runningGameAdapterCancellationTokenSource?.Cancel();
    Console.WriteLine("Listening for games");
};

Console.WriteLine("Listening for games");

var statusManagerCancellationToken = new CancellationToken();
await statusManager.Run(statusManagerCancellationToken);

void PrintTraces(string name, Traces traces) {
    Console.Clear();
    Console.WriteLine(name);
    Console.WriteLine($"Throttle:\t {ConsoleHelper.PrintLoading(traces.Throttle)} {traces.Throttle}");
    Console.WriteLine($"Brake:\t\t {ConsoleHelper.PrintLoading(traces.Brake)} {traces.Brake}");
    Console.WriteLine($"Clutch:\t\t {ConsoleHelper.PrintLoading(traces.Clutch)} {traces.Clutch}");
    Console.WriteLine($"Steering:\t {ConsoleHelper.PrintRange(traces.Steering)} {traces.Steering}");
}
