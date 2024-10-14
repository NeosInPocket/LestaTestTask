using System;
using System.Threading.Tasks;

public class GameCameraLoadingOperation : ILoadingOperation
{
    public string Description => "Loading camera";

    public async Task Load(Action<float> onUpdate)
    {
        onUpdate?.Invoke(0.25f);

        await Task.Delay(300);
        onUpdate?.Invoke(0.65f);

        var loader = new LocalContentLoader();
        await loader.LoadAndInstantiate<CameraBehaviour>("GameCamera");

        onUpdate?.Invoke(1f);
    }
}