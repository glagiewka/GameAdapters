using GameAdapters.Adapters.Extensions;
using GameAdapters.Adapters.Models;

namespace GameAdapters.Adapters.AssettoCorsa;

public class AssettoCorsaAdapter : IGameAdapter
{
    public event EventHandler<Traces>? TracesChanged;
    public string Name => "Assetto Corsa";

    public async Task Run(CancellationToken cancellationToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));

        while (true)
        {
            var physicsData = SharedMemoryReader.ReadPhysics();
            if (physicsData is not null)
            {
                var data = physicsData.Value;
                var traces = new Traces
                {
                    Throttle = data.Gas,
                    Brake = data.Brake,
                    Clutch = data.Clutch,
                    Steering = data.SteerAngle,
                    Timestamp = DateTime.UtcNow.GetMillisecondsSinceEpoch()
                };

                TracesChanged?.Invoke(null, traces);
            }

            await timer.WaitForNextTickAsync(cancellationToken);
        }
    }
}