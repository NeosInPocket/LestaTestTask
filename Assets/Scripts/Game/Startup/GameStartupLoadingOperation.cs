using System;
using System.Threading.Tasks;

public class GameStartupLoadingOperation : ILoadingOperation
{
    private GameStartup gameStartup;
    public string Description => "Starting game";

    public async Task Load(Action<float> onUpdate)
    {
        onUpdate?.Invoke(0.45f);

        var loader = new LocalContentLoader();
        gameStartup = await loader.LoadAndInstantiate<GameStartup>("GameStartup");

        onUpdate?.Invoke(1f);
    }
}