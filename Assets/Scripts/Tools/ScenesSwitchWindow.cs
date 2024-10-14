using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitchWindow : EditorWindow
{
    private void OnGUI()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;

        for (var i = 0; i < sceneCount; i++)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(i);

            if (GUILayout.Button(GetSceneNameFromPath(path)))
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(path);
            }
        }
    }

    [MenuItem("Tools/Scenes switch window")]
    public static void ShowWindow()
    {
        var window = (ScenesSwitchWindow)GetWindow(typeof(ScenesSwitchWindow));
        window.minSize = new Vector2(100, 100);
        window.maxSize = new Vector2(300, 300);
    }

    private string GetSceneNameFromPath(string path)
    {
        var slashIndex = path.LastIndexOf('/');
        var dotIndex = path.LastIndexOf('.');

        var nameWithoutExtension = path.Substring(0, dotIndex);
        var name = nameWithoutExtension.Substring(slashIndex + 1, nameWithoutExtension.Length - slashIndex - 1);
        return name;
    }
}