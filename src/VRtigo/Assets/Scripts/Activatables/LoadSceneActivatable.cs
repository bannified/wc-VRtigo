using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private string m_SceneName;

    public void Activate()
    {
        if (SceneTransistor.Instance != null)
        {
            SceneTransistor.Instance.FadeToScene(m_SceneName);
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneName);
        }
    }
}
