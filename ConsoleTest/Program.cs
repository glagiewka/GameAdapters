using ConsoleTest;
using GameAdapters.Adapters;

Console.WriteLine("Listening for games");
var gameManager = new GameAdapterManager();

gameManager.GameStarted += (sender, args) => Console.WriteLine($"{args.Name} started");
gameManager.GameEnded += (sender, name) => Console.WriteLine($"{name} ended");
gameManager.TracesChanged += (sender, args) => PrintTraces(args);

await gameManager.Run();

void PrintTraces(Traces traces)
{
    Console.Clear();
    //Console.WriteLine(traces);
    Console.WriteLine($"Throttle:\t {ConsoleHelper.PrintLoading(traces.Throttle)} {traces.Throttle}");
    Console.WriteLine($"Brake:\t\t {ConsoleHelper.PrintLoading(traces.Brake)} {traces.Brake}");
    Console.WriteLine($"Clutch:\t\t {ConsoleHelper.PrintLoading(traces.Clutch)} {traces.Clutch}");
    Console.WriteLine($"Steering:\t {ConsoleHelper.PrintRange(traces.Steering)} {traces.Steering}");
}