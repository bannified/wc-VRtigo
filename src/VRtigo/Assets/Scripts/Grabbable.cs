using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public System.Action<Grabbable> onGrab;
    public System.Action<Grabbable> onDrop;

    public System.Action<Grabbable> onNonInteractable;
    public System.Action<Grabbable> OnInteractable;

    public SoundData m_GrabSound;
    public SoundData m_DropSound;

    public bool m_IsGrabbed;

    [SerializeField]
    private int m_NumInContact = 0;

    private void Start()
    {
        if (m_GrabSound)
            AudioManager.InitAudioSourceOn(m_GrabSound, this.gameObject);

        if (m_DropSound)
            AudioManager.InitAudioSourceOn(m_DropSound, this.gameObject);
    }

    public virtual void Grabbed()
    {
        m_NumInContact++;

        // First to grab
        if (m_NumInContact == 1)
        {
            m_IsGrabbed = true;

            if (m_GrabSound)
                m_GrabSound.m_Source.Play();

            onGrab?.Invoke(this);
        }
    }

    public virtual void Dropped()
    {
        m_NumInContact--;

        // Last to grab
        if (m_NumInContact == 0)
        {
            m_IsGrabbed = false;

            if (m_DropSound)
                m_DropSound.m_Source.Play();

            onDrop?.Invoke(this);
        }
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
}
