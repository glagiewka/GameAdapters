namespace GameAdapters.Adapters.AssettoCorsa;

public class AssettoCorsaStatusAdapter : IGameStatusAdapter {
    public event EventHandler? Activated;
    public event EventHandler? Deactivated;
    private bool _isActive = false;

    public bool IsActive() {
        return _isActive;
    }

    public GameInfo GetGameInfo() {
        var staticData = GetStaticData();

        if (!staticData.HasValue) {
            throw new InvalidOperationException("Unable to read game info if a non active game");
        }

        return new() {
            Name = "Assetto Corsa",
                 Version = staticData.Value.ACVersion
        };
    }

    public async Task Run(CancellationToken cancellationToken) {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while(true) {
            var staticData = GetStaticData();

            if (!_isActive && staticData is not null) {
                _isActive = true;
                Activated?.Invoke(null, EventArgs.Empty);
            } 
            if (_isActive && staticData is null) {
                _isActive = false;
                Deactivated?.Invoke(null, EventArgs.Empty);
            }

            await timer.WaitForNextTickAsync(cancellationToken);
        }
    }

    private Static? GetStaticData() {
        return SharedMemoryReader.ReadStatic();
    }
}
