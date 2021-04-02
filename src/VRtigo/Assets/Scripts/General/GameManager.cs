using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static GameManager _instance;
    public static GameManager Instance
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

    private static GameManager CreateInstance()
    {
        GameObject go = new GameObject();
        go.name = "GameManager";
        GameManager gameManager = go.AddComponent<GameManager>();
        return gameManager;
    }
    #endregion

    /// <summary>
    /// List of Registered PlayerControllers.
    /// By default, PlayerControllers should be registering themselves with the GameManager on Start.
    /// </summary>
    [SerializeField]
    private List<PlayerController> m_PlayerControllers = new List<PlayerController>();

    private Dictionary<PlayerController, Character> m_ControllerToCharacterMap = new Dictionary<PlayerController, Character>();

    [SerializeField]
    private ExperienceData m_CurrentExperience;

    public void StartNextExperience()
    {
        if (m_CurrentExperience == null)
        {
            return;
        }

        StartExperience(m_CurrentExperience.NextExperienceData);
    }

    public void StartExperience()
    {
        if (m_CurrentExperience == null)
        {
            return;
        }

        if (m_CurrentExperience.ExperienceScene != null)
        {
            m_CurrentExperience.UpdateSettings(PersistenceManager.Instance);

            SceneTransistor.Instance.FadeViaBridge(m_CurrentExperience.ExperienceScene);
        }
    }

    public void StartExperience(ExperienceData experienceData)
    {
        SetCurrentExperience(experienceData);
        StartExperience();
    }

    public void SetCurrentExperience(ExperienceData experienceData)
    {
        m_CurrentExperience = experienceData;
    }

    public ExperienceData GetCurrentExperience()
    {
        return m_CurrentExperience;
    }

    public void RegisterPlayerController(PlayerController controller)
    {
        m_PlayerControllers.Add(controller);
        m_ControllerToCharacterMap.Add(controller, null);
    }

    public void RegisterCharacterToController(PlayerController controller, Character character)
    {
        m_ControllerToCharacterMap[controller] = character;
    }

    public void UnregisterCharacterFromController(PlayerController controller, Character character)
    {
        if (m_ControllerToCharacterMap[controller] == character)
        {
            m_ControllerToCharacterMap[controller] = null;
        }
    }

    public void UnregisterPlayerController(PlayerController controller)
    {
        m_ControllerToCharacterMap.Remove(controller);
        m_PlayerControllers.Remove(controller);
    }

    /// <summary>
    /// Returns the Character assigned to the controller of <code>controllerIndex</code>.
    /// Can be null.
    /// </summary>
    /// <param name="controllerIndex">Index of the desired Controller</param>
    public Character GetCharacter(int controllerIndex)
    {
        if (m_PlayerControllers.Count > controllerIndex)
        {
            PlayerController pc = m_PlayerControllers[controllerIndex];

            Character result;
            m_ControllerToCharacterMap.TryGetValue(pc, out result);
            return result;
        }
        else
        {
            return null;
        }

    }
}
