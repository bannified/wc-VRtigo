using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : Grabbable
{
    [SerializeField]
    protected ExperienceData m_ExperienceData;

    [SerializeField]
    protected List<SoundData> m_Musics;

    public ExperienceData GetExperience() { return m_ExperienceData; }

    public List<SoundData> GetMusics() { return m_Musics; }
}
