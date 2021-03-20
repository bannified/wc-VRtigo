using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MultisceneBootstrapper))]
public class MultisceneBootstrapperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MultisceneBootstrapper bootstrapper = target as MultisceneBootstrapper;

        if (target == null)
        {
            return;
        }

        if (GUILayout.Button("Load All Scenes Additive"))
        {
            LoadAllAdditiveScenes_Editor(bootstrapper);
        }

        if (GUILayout.Button("Unload All Other Scenes"))
        {
            UnloadAllOtherScenes(bootstrapper);
        }
    }

    public void LoadAllAdditiveScenes_Editor(MultisceneBootstrapper castedTarget)
    {
        EditorSceneManager.SetActiveScene(castedTarget.gameObject.scene);

        foreach (SceneReference sceneRef in castedTarget.GetAllAdditiveScenes())
        {
            EditorSceneManager.OpenScene(sceneRef.ScenePath, OpenSceneMode.Additive);
        }
    }

    public void UnloadAllOtherScenes(MultisceneBootstrapper castedTarget)
    {
        UnityEngine.SceneManagement.Scene currentScene = castedTarget.gameObject.scene;

        foreach (UnityEngine.SceneManagement.Scene scene in EditorSceneManager.GetAllScenes())
        {
            if (currentScene == scene)
            {
                continue;
            }

            EditorSceneManager.CloseScene(scene, true);
        }
    }

}
