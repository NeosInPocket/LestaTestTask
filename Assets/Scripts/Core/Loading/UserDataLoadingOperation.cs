using System;
using System.Threading.Tasks;

public class UserDataLoadingOperation : ILoadingOperation
{
    private Action<float> onProgressUpdate;
    private UserDataContainer userDataContainer;

    public UserDataLoadingOperation(UserDataContainer container)
    {
        userDataContainer = container;
    }

    public string Description => "Loading user data...";

    public async Task Load(Action<float> onUpdate)
    {
        onProgressUpdate?.Invoke(0.5f);
        onProgressUpdate = onUpdate;

        var saveSystem = new SaveSystem<UserData>();
        await saveSystem.LoadAsset();

        onProgressUpdate?.Invoke(1f);
    }
}