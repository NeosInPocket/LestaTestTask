using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingOperation : ILoadingOperation
{
    private float loadingProgress;
    private Action<float> onProgressUpdate;
    private AsyncOperation sceneLoadOperation;

    public SceneLoadingOperation(string sceneName)
    {
        SceneName = sceneName;
    }

    public string SceneName { get; set; }
    public string Description => "Loading game scene";

    public async Task Load(Action<float> onUpdate)
    {
        sceneLoadOperation = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

        onProgressUpdate = onUpdate;
        loadingProgress = 0;

        await WaitForSceneLoad();
    }

    private async Task WaitForSceneLoad()
    {
        while (sceneLoadOperation.progress < 1f)
        {
            if (loadingProgress < sceneLoadOperation.progress)
            {
                loadingProgress = sceneLoadOperation.progress;
                onProgressUpdate(loadingProgress);
            }

            await Task.Yield();
        }
    }
}