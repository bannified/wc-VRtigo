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
    private List<PlayerController> m_PlayerControllers =  new List<PlayerController>();

    private Dictionary<PlayerController, Character> m_ControllerToCharacterMap = new Dictionary<PlayerController, Character>();

    public void RegisterPlayerController(PlayerController controller)
    {
        m_PlayerControllers.Add(controller);
        m_ControllerToCharacterMap.Add(controller, null);
    }

    public void UnregisterPlayerController(PlayerController controller)
    {
        m_ControllerToCharacterMap.Remove(controller);
        m_PlayerControllers.Remove(controller);
    }
}
