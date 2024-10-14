using UnityEngine;

public class AppContext
{
    public static AppContext Instance;
    private string loadingScreenAssetId => "UILoadingScreen";
    public UserDataContainer UserDataContainer { get; }
    public UILoadingScreen UILoadingScreen { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static async void LoadMenu()
    {
        Instance = new AppContext();

        var loader = new LocalContentLoader();
        Instance.UILoadingScreen = await loader.LoadAndInstantiate<UILoadingScreen>(Instance.loadingScreenAssetId);
    }
}