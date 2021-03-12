using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomPayloadSetter : MonoBehaviour, IActivatable
{
    [SerializeField]
    private ClassroomManagerPayload m_PayloadToSet;

    [SerializeField]
    private bool m_SetOnStart = false;

    /// <summary>
    /// [TODO]: Add in other ways to set the payload for classroom while in an experience or even in menus (if applicable)
    /// </summary>

    private void Start()
    {
        if (m_SetOnStart)
        {
            SetPayload();
        }
    }

    public void Activate()
    {
        SetPayload();
    }

    private void SetPayload()
    {
        GameManager.Instance.SetClassroomManagerPayload(m_PayloadToSet);
    }
}
