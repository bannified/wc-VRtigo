using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grabbable : MonoBehaviour
{
    public UnityEvent m_GrabbedEvent { get; private set; }
    public UnityEvent m_DroppedEvent { get; private set; }

    public bool m_IsGrabbed { get; private set; }

    private void Start()
    {
        m_GrabbedEvent = new UnityEvent();
        m_DroppedEvent = new UnityEvent();
    }

    public virtual void Grabbed()
    {
        m_IsGrabbed = true;
        m_GrabbedEvent.Invoke();
    }
    public virtual void Dropped()
    {
        m_IsGrabbed = false;
        m_DroppedEvent.Invoke();
    }

    // Misc
    public bool GetIsGrabbed() { return m_IsGrabbed; }
}
