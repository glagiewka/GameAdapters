using GameAdapters.Adapters.Models;

namespace GameAdapters.Adapters;

public interface IGameStatusAdapter
{
    bool IsActive { get; }
    string Name { get; }
    event EventHandler Activated;
    event EventHandler Deactivated;
    GameInfo GetGameInfo();
    Task Run(CancellationToken cancellationToken);
}