using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public System.Action<Grabbable> onGrab;
    public System.Action<Grabbable> onDrop;

    public System.Action<Grabbable> onNonInteractable;
    public System.Action<Grabbable> OnInteractable;

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

    /**
     * Used when the grabbable object is no longer interactable
     * e.g. Vinyl disc is set on the record player, hence no longer grabbable by the player
     */
    public void SetNonInteractable()
    {
        onNonInteractable?.Invoke(this);
    }


    /**
     * Used when the grabbable object is interactable
     * e.g. Vinyl disc is set off the record player, hence grabbable by the player
     */
    public void SetInteractable()
    {
        OnInteractable?.Invoke(this);
    }

    // Misc
    public bool GetIsGrabbed() { return m_IsGrabbed; }
}
