using GameAdapters.Adapters;
using GameAdapters.Adapters.AssettoCorsa;

var statusManager = new GameStatusManager();
statusManager.AddAdapter(new AssettoCorsaStatusAdapter());
statusManager.Activated += (sender, args) => Console.WriteLine($"{args.Name} connected");
statusManager.Deactivated += (sender, args) => Console.WriteLine($"{args.Name} disconnected");

Console.WriteLine("Listening for games");

var cancellationToken = new CancellationToken();
await statusManager.Run(cancellationToken);
