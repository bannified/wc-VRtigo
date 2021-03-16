using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static PersistenceManager _instance;
    public static PersistenceManager Instance
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
    private StringToStringDictionary StringDictionary;
    public bool TryGetString(string key, ref string value)
    {
        return StringDictionary.TryGetValue(key, out value);
    }
    public void SetString(string key, string value)
    {
        StringDictionary[key] = value;
    }

    [SerializeField]
    private StringToBoolDictionary BoolDictionary;
    public bool TryGetBool(string key, ref bool value)
    {
        return BoolDictionary.TryGetValue(key, out value);
    }
    public void SetBool(string key, bool value)
    {
        BoolDictionary[key] = value;
    }

    [SerializeField]
    private StringToIntDictionary IntDictionary;

    public bool TryGetInt(string key, ref int value) 
    {
        return IntDictionary.TryGetValue(key, out value);
    }
    public void SetInt(string key, int value)
    {
        IntDictionary[key] = value;
    }
}
