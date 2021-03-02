using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion

    [SerializeField]
    protected Animator m_SceneTransistorAnimator;

    [SerializeField]
    protected Camera m_PlayerCamera;

    [SerializeField]
    protected Canvas m_Canvas;

    private string m_SceneName = "SampleScene";

    private void Update()
    {
        if (m_PlayerCamera == null)
        {
            return;
        }

        Vector3 cameraForward = m_PlayerCamera.transform.forward;

        // Make sure entire canvas is always in front of camera and rotated correctly
        transform.position = m_PlayerCamera.transform.position + cameraForward * 0.00001f;
        transform.LookAt(2 * transform.position - m_PlayerCamera.transform.position);
    }

    /**
     * Fade the screen to black and load the scene with the given name.
     * sceneName:		name of scene file to be loaded
     * sceneTitle:		name of scene to be shown to the player during transition
     * sceneTitlePos:	world position of where should the title be shown
     * sceneTitleDur:	duration in which the scene of title to be shown (in seconds), including fadeIn time
     */
    public void FadeToScene(string sceneName)
    {
        m_PlayerCamera = Camera.main;
        m_Canvas.worldCamera = m_PlayerCamera;

        this.gameObject.SetActive(true);
        m_SceneName = sceneName;

        // Start animation
        m_SceneTransistorAnimator.SetTrigger("FadeOut");
    }

    /**
     * This is called from the FadeOut animation.
     */
    public void LoadScene()
    {
        SceneManager.LoadScene(m_SceneName);
        m_SceneTransistorAnimator.SetTrigger("FadeIn");
        m_PlayerCamera = Camera.main;
        m_Canvas.worldCamera = m_PlayerCamera;
    }
}
