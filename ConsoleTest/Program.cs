using ConsoleTest;
using GameAdapters.Adapters;
using GameAdapters.Adapters.Models;

Console.WriteLine("Listening for games");
var gameManager = new GameAdapterManager();

gameManager.GameStarted += (_, args) => Console.WriteLine($"{args.Name} started");
gameManager.GameEnded += (_, name) => Console.WriteLine($"{name} ended");
gameManager.TracesChanged += (_, args) => PrintTraces(args);

await gameManager.Run();
return;

void PrintTraces(Traces traces)
{
    Console.Clear();
    Console.WriteLine(traces);
    Console.WriteLine($"Throttle:\t {ConsoleHelper.PrintLoading(traces.Throttle)} {traces.Throttle}");
    Console.WriteLine($"Brake:\t\t {ConsoleHelper.PrintLoading(traces.Brake)} {traces.Brake}");
    Console.WriteLine($"Clutch:\t\t {ConsoleHelper.PrintLoading(traces.Clutch)} {traces.Clutch}");
    Console.WriteLine($"Steering:\t {ConsoleHelper.PrintRange(traces.Steering)} {traces.Steering}");
}