using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceSettingActivatable : MonoBehaviour, IActivatable
{
    [Header("Allows you to setup parameters for your experience to load before it starts")]
    [SerializeField]
    public StringToBoolDictionary BoolSettings;

    [SerializeField]
    public StringToIntDictionary IntSettings;

    [SerializeField]
    public StringToStringDictionary StringSettings;

    public void Activate()
    {
        PersistenceManager persistenceManager = PersistenceManager.Instance;

        if (BoolSettings != null)
        {
            foreach (var kv in BoolSettings)
            {
                persistenceManager.SetBool(kv.Key, kv.Value);
            }
        }

        if (IntSettings != null)
        {
            foreach (var kv in IntSettings)
            {
                persistenceManager.SetInt(kv.Key, kv.Value);
            }
        }

        if (StringSettings != null)
        {
            foreach (var kv in StringSettings)
            {
                persistenceManager.SetString(kv.Key, kv.Value);
            }
        }
    }
}
