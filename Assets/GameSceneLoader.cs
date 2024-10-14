using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameSceneLoader : MonoBehaviour
{
    public void LoadSceneAsync(string sceneName)
    {
        LoadScene(sceneName);
    }

    private async Task LoadScene(string sceneName)
    {
        var screen = AppContext.Instance.UILoadingScreen;
        var gameContext = new GameContext();

        var loadingOperations = new Queue<ILoadingOperation>();

        loadingOperations.Enqueue(new SceneLoadingOperation(sceneName));

        if (sceneName == "Game")
            foreach (var loadingOperation in gameContext.GameLoadingOperations)
                loadingOperations.Enqueue(loadingOperation);

        await screen.Load(loadingOperations);
    }
}