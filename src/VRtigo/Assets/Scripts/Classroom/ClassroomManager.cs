using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ClassroomManager : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static ClassroomManager _instance;
    public static ClassroomManager Instance
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

    private void OnDestroy()
    {
        _instance = null;
    }
    #endregion

    private void Start()
    {
        if (SteamVR.active)
        {
            VRT_Helpers.ResetHMDPosition();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneTransistor.Instance.FadeToScene("Level_PlayerInitiatedMovement");
        }
    }
}
