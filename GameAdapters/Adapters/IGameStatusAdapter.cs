namespace GameAdapters.Adapters;

public interface IGameStatusAdapter {
    event EventHandler Activated;
    event EventHandler Deactivated;
    bool IsActive();
    GameInfo GetGameInfo();
    Task Run(CancellationToken cancellationToken);
}
