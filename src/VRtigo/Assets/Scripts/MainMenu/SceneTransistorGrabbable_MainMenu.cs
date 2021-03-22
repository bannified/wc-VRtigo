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

    public void Grabbed() { m_IsGrabbed = true; }

    public void Dropped() { m_IsGrabbed = false; }

    public ExperienceData GetExperience() { return m_ExperienceData; }

    public string GetMusicName() { return m_MusicName; }

    public bool GetIsGrabbed() { return m_IsGrabbed; }
}
