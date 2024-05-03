using GameAdapters.Adapters.AssettoCorsa;

var adapter = new AssettoCorsaStatusAdapter();
var cancellationToken = new CancellationToken();

adapter.Activated += (sender, args) => Console.WriteLine("AC connected");
adapter.Deactivated += (sender, args) => Console.WriteLine("AC disconnected");

Console.WriteLine("Listening for games");
await adapter.Run(cancellationToken);

