using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public System.Action<Grabbable> onGrab;
    public System.Action<Grabbable> onDrop;

    public bool m_IsGrabbed { get; private set; }

    public virtual void Grabbed()
    {
        m_IsGrabbed = true;
        onGrab?.Invoke(this);
    }
    public virtual void Dropped()
    {
        m_IsGrabbed = false;
        onDrop?.Invoke(this);
    }

    // Misc
    public bool GetIsGrabbed() { return m_IsGrabbed; }
}
