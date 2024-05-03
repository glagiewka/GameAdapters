namespace GameAdapters.Adapters.AssettoCorsa;

public class AssettoCorsaStatusAdapter : IGameStatusAdapter {
    private const string GAME_NAME = "Assetto Corsa";

    public event EventHandler? Activated;
    public event EventHandler? Deactivated;
    public bool IsActive { get; private set; }
    public string Name => GAME_NAME;

    public GameInfo GetGameInfo() {
        var staticData = GetStaticData();

        if (!staticData.HasValue) {
            throw new InvalidOperationException("Unable to read game info of a non active game");
        }

        return new() {
            Name = GAME_NAME, 
                 Version = staticData.Value.ACVersion
        };
    }

    public async Task Run(CancellationToken cancellationToken) {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while(true) {
            var staticData = GetStaticData();

            if (!IsActive && staticData is not null) {
                IsActive = true;
                Activated?.Invoke(this, EventArgs.Empty);
            } 
            if (IsActive && staticData is null) {
                IsActive = false;
                Deactivated?.Invoke(this, EventArgs.Empty);
            }

            await timer.WaitForNextTickAsync(cancellationToken);
        }
    }

    private Static? GetStaticData() {
        return SharedMemoryReader.ReadStatic();
    }
}
