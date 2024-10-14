using System.Collections.Generic;
using Game.Startup;

public class GameContext
{
    public readonly GameAssetsLoadingOperation assetsLoadingOperation;
    public readonly CharacterLoadingOperation characterLoadingOperation;
    private readonly GameCameraLoadingOperation gameCameraLoadingOperation;
    private readonly GameStartupLoadingOperation gameStartupLoadingOperation;

    public GameContext()
    {
        Instance = this;

        GameLoadingOperations = new Queue<ILoadingOperation>();

        assetsLoadingOperation = new GameAssetsLoadingOperation();
        characterLoadingOperation = new CharacterLoadingOperation();
        gameStartupLoadingOperation = new GameStartupLoadingOperation();
        gameCameraLoadingOperation = new GameCameraLoadingOperation();

        GameLoadingOperations.Enqueue(assetsLoadingOperation);
        GameLoadingOperations.Enqueue(gameStartupLoadingOperation);
        GameLoadingOperations.Enqueue(characterLoadingOperation);
        GameLoadingOperations.Enqueue(gameCameraLoadingOperation);
    }

    public Queue<ILoadingOperation> GameLoadingOperations { get; }
    public static GameContext Instance { get; private set; }
}