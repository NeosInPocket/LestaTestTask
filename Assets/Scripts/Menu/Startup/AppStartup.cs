using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppStartup : MonoBehaviour
{
    [SerializeField] private string loadingScreenAssetId;
    public static AppStartup Instance { get; private set; }

    private async void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        DontDestroyOnLoad(gameObject);
        await SetLoadingScreen();
    }

    public async Task SetLoadingScreen()
    {
        var loadingOperations = new Queue<ILoadingOperation>();
        loadingOperations.Enqueue(new UserDataLoadingOperation(AppContext.Instance.UserDataContainer));

        if (SceneManager.GetActiveScene().name != "Menu") loadingOperations.Enqueue(new SceneLoadingOperation("Menu"));

        if (AppContext.Instance.UILoadingScreen == null) await WaitForLoadingScreen();

        await AppContext.Instance.UILoadingScreen.Load(loadingOperations);
    }

    private async Task WaitForLoadingScreen()
    {
        while (AppContext.Instance.UILoadingScreen == null) await Task.Yield();
    }
}