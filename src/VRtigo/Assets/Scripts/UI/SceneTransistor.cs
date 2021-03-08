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
        _instance = this;
    }
    #endregion

    [SerializeField]
    protected Animator m_SceneTransistorAnimator;

    [SerializeField]
    protected Camera m_PlayerCamera;

    [SerializeField]
    protected Canvas m_Canvas;

    private string m_SceneName = "SampleScene";

    private void Start()
    {
        m_PlayerCamera = Camera.main;
        if (m_PlayerCamera == null)
        {
            Debug.LogError("There is no camera tagged as MainCamera");
        }

        //m_SceneTransistorAnimator = GetComponent<Animator>();

        //// Reset transform
        //transform.position = Vector3.zero;
        //transform.rotation = Quaternion.identity;

        //// Set it to world space
        //Canvas fadeCanvas = GetComponent<Canvas>();
        //fadeCanvas.renderMode = RenderMode.WorldSpace;
        //fadeCanvas.worldCamera = m_PlayerCamera;

        //Vector3 cameraForward = m_PlayerCamera.transform.forward;
        //cameraForward.y = 0.0f;

        // Make sure black screen is always in front of camera and rotated correctly
        //Image blackScreen = GetComponentInChildren<Image>();
        //blackScreen.transform.position = m_PlayerCamera.transform.position + cameraForward;
        //blackScreen.transform.rotation = Quaternion.LookRotation(cameraForward);

        // Set camera as parent, so it moves as camera moves
        transform.SetParent(m_PlayerCamera.transform);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
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
    }
}
