namespace GameAdapters.Adapters;

public interface IGameStatusAdapter {
    event EventHandler Activated;
    event EventHandler Deactivated;
    bool IsActive { get; }
    string Name { get; }
    GameInfo GetGameInfo();
    Task Run(CancellationToken cancellationToken);
}
