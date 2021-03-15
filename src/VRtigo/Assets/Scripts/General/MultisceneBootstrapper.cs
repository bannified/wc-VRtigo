using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultisceneBootstrapper : MonoBehaviour
{
    [SerializeField]
    private List<SceneReference> m_AdditiveScenes;

    private void Start()
    {
        LoadAllAdditiveScenes();
    }

    public void LoadAllAdditiveScenes()
    {
        foreach (SceneReference sceneRef in m_AdditiveScenes)
        {
            int buildIndex = UnityEngine.SceneManagement.SceneUtility.GetBuildIndexByScenePath(sceneRef.ScenePath);

            Scene scene = SceneManager.GetSceneByBuildIndex(buildIndex);

            if (scene.isLoaded)
            {
                continue;
            }

            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        }
    }

    public List<SceneReference> GetAllAdditiveScenes() { return m_AdditiveScenes; }
}
