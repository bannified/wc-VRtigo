using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private SceneReference m_SceneRef;

    public void Activate()
    {
        if (SceneTransistor.Instance != null)
        {
            SceneTransistor.Instance.FadeToScene(m_SceneRef);
        } else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneRef);
        }
    }
}
