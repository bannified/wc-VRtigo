using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSetIntActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private string m_Key;

    [SerializeField]
    private int m_Value; 

    public void Activate()
    {
        PlayerPrefs.SetInt(m_Key, m_Value);
    }
}
