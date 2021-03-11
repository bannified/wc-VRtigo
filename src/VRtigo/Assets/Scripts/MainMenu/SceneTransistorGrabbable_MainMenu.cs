using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : MonoBehaviour, IGrabbable
{
    [SerializeField]
    protected RecordPlayer_MainMenu m_RecordPlayer;
    [SerializeField]
    protected string m_SceneName;
    [SerializeField]
    protected string m_MusicName;

    public void Grabbed() { }

    public void Dropped() { }

    public string GetSceneName() { return m_SceneName; }
    public string GetMusicName() { return m_MusicName; }
}
