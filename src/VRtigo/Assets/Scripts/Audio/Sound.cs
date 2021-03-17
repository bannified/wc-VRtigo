using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]
    protected string m_Name;
    [SerializeField]
    protected AudioClip m_Clip;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float volume = 0.75f;
    [Range(0f, 1f)]
    public float volumeVariance = 0.1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = 0.0f;

    [Range(0f, 1f)]
    public float spatialBlend = 0.2f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource m_Source;

    public string GetName() { return m_Name; }
    public AudioClip GetClip() { return m_Clip; }
}