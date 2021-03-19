using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : Grabbable
{
    [SerializeField]
    protected ExperienceData m_ExperienceData;

    [SerializeField]
    protected string m_MusicName;

    public ExperienceData GetExperience() { return m_ExperienceData; }

    public string GetMusicName() { return m_MusicName; }
}
