using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRT_Constants.BridgeConstants;

public class SceneTransistor : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static SceneTransistor _instance;
    public static SceneTransistor Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return null;
#endif

            lock (_lock)
            {
                return _instance;
            }

        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public SceneReference m_BridgeScene;

    [SerializeField]
    protected Camera m_PlayerCamera;

    [SerializeField]
    protected Image m_BlackScreen;

    [SerializeField]
    protected float m_FadeInDur = 1.5f;

    [SerializeField]
    protected float m_FadeOutDur = 1.5f;

    private void Start()
    {
        m_PlayerCamera = Camera.main;
        if (m_PlayerCamera == null)
        {
            Debug.LogError("There is no camera tagged as MainCamera");
        }

        // Set camera as parent, so it moves as camera moves
        transform.SetParent(m_PlayerCamera.transform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Set alpha to 1
        Color imageColor = m_BlackScreen.color;
        imageColor.a = 1.0f;
        m_BlackScreen.color = imageColor;

        StartCoroutine(Image_Utils.FadeOutImageCoroutine(m_BlackScreen, m_FadeOutDur));
    }

    /**
     * Fade the screen to black and load the scene with the given name.
     * sceneName:		name of scene file to be loaded
     */
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeToSceneEnum(sceneName));
    }

    public void FadeViaBridge(string sceneName)
    {
        PersistenceManager.Instance.SetString(BridgeConstants.NEXT_EXPERIENCE_REF, sceneName);

        StartCoroutine(FadeToSceneEnum(m_BridgeScene));
    }

    IEnumerator FadeToSceneEnum(string sceneName)
    {
        yield return StartCoroutine(Image_Utils.FadeInImageCoroutine(m_BlackScreen, m_FadeOutDur));
        SceneManager.LoadScene(sceneName);
    }
}
