using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : MonoBehaviour, IGrabbable
{
    [SerializeField]
    protected ExperienceData m_ExperienceData;
    [SerializeField]
    protected string m_MusicName;
    [SerializeField]
    protected bool m_IsGrabbed;
    [SerializeField]
    protected IconTrigger m_IconTrigger;

    public void Grabbed()
    {
        m_IsGrabbed = true;

        // Once grabbed, the icon should not appear
        if (m_IconTrigger != null)
            m_IconTrigger.DisableIcon();
    }

    public void Dropped()
    {
        m_IsGrabbed = false;

        // Once let go, icon may appear again
        if (m_IconTrigger != null)
            m_IconTrigger.EnableIcon();
    }

    public ExperienceData GetExperience() { return m_ExperienceData; }

    public string GetMusicName() { return m_MusicName; }

    public bool GetIsGrabbed() { return m_IsGrabbed; }
}
