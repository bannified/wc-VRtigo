using System.Collections;
using UnityEngine;
using VRT_Constants.BridgeConstants;

public class Level_Bridge : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static Level_Bridge _instance;
    public static Level_Bridge Instance
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
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private static Level_Bridge CreateInstance()
    {
        GameObject go = new GameObject();
        go.name = "Level_Bridge";
        Level_Bridge audioManager = go.AddComponent<Level_Bridge>();
        return audioManager;
    }
    #endregion

    public string m_TitleText = "Default Title";
    public float m_TitleDur = 3.0f;

    [SerializeField]
    private AnimatedText m_AnimatedTitleText;

    void Start()
    {
        Debug.Log("LEVEL_BRIDGE ACTIVATED");
        StartCoroutine(BridgeSequence());
    }

    IEnumerator BridgeSequence()
    {
        // Use coroutine to delay until first frame, to ensure player is loaded in and other scripts are ready
        yield return new WaitForEndOfFrame();

        // Set screen title
        m_AnimatedTitleText.SetStringText(m_TitleText);
        string titleText = null;
        bool success = PersistenceManager.Instance.TryGetString(BridgeConstants.EXPERIENCE_TITLE, ref titleText);

        if (success)
            m_AnimatedTitleText.SetStringText(titleText);

        // Begin text animation
        m_AnimatedTitleText.TransitionIn();

        // Wait for fixed duration, and fade out title
        yield return new WaitForSeconds(m_TitleDur);
        m_AnimatedTitleText.TransitionOut();

        // Finally, transition to the correct scene
        string nextExpRef = null;
        success = PersistenceManager.Instance.TryGetString(BridgeConstants.NEXT_EXPERIENCE_REF, ref nextExpRef);

        if (success)
            SceneTransistor.Instance.FadeToScene(nextExpRef);
    }
}
