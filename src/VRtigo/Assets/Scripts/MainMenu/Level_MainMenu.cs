using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_MainMenu : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static Level_MainMenu _instance;
    public static Level_MainMenu Instance
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

    private static Level_MainMenu CreateInstance()
    {
        GameObject go = new GameObject();
        go.name = "Level_MainMenu";
        Level_MainMenu levelMainMenu = go.AddComponent<Level_MainMenu>();
        return levelMainMenu;
    }
    #endregion

    public List<string> m_MainMenuBGMs;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayBackgroundMusics(Instance.m_MainMenuBGMs);
    }
}
