using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : Grabbable
{
    [SerializeField]
    protected ExperienceData m_ExperienceData;

    [SerializeField]
    protected List<string> m_MusicNames;

    public ExperienceData GetExperience() { return m_ExperienceData; }

    public List<string> GetMusicNames() { return m_MusicNames; }
}
