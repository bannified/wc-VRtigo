using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    protected List<Sound> m_Sounds;

    [SerializeField]
    private Hashtable m_SoundsInternal;

    [SerializeField]
    private float m_VolumeStep = 0.01f;
    
    [SerializeField]
    private float m_FadeDuration = 2.0f;

    [SerializeField]
    private string m_BackgroundMusicName;

    #region Singleton Implementation and Initialisation
    private static object _lock = new object();
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return null;
#endif

            lock (_lock)
            {
                return _instance;
            }

        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        m_SoundsInternal = new Hashtable();
        foreach (Sound s in m_Sounds)
        {
            s.m_Source = gameObject.AddComponent<AudioSource>();
            s.m_Source.clip = s.GetClip();
            s.m_Source.loop = s.loop;

            try
            {
                m_SoundsInternal.Add(s.GetName(), s);
            } catch
            {
                Debug.LogWarning(string.Format("Audio Manager: Duplicate clip name '{0}' detected", s.GetName()));
            }
        }
    }

    private static AudioManager CreateInstance()
    {
        GameObject go = new GameObject();
        go.name = "AudioManager";
        AudioManager audioManager = go.AddComponent<AudioManager>();
        return audioManager;
    }
    #endregion

    public void Play(string soundName)
    {
        Sound s = GetSound(soundName);

        s.m_Source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.m_Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.m_Source.Play();
    }

    public void PlayBackgroundMusic(string soundName)
    {
        // Currently there is a background music playing
        if (m_BackgroundMusicName != null && m_BackgroundMusicName != "")
        {
            StartCoroutine("FadeOutSound", m_BackgroundMusicName);
        }

        m_BackgroundMusicName = soundName;

        // No new background music
        if (soundName == "")
            return;

        // Play the new background music
        Play(soundName);
    }

    private Sound GetSound(string soundName)
    {
        // TODO: Handle when soundName does not exist
        return m_SoundsInternal[soundName] as Sound;
    }

    IEnumerator FadeOutSound(string soundName)
    {
        Sound s = GetSound(soundName);

        if (!s.m_Source.isPlaying)
            yield break;

        float durationSoFar = 0.0f;
        float totalDuration = m_FadeDuration;
        float progress = 0.0f;
        float oriVol = s.m_Source.volume;

        while (s.m_Source.volume > 0.0f)
        {
            progress = durationSoFar / totalDuration;
            s.m_Source.volume = (1.0f - progress - Time.deltaTime / totalDuration) * oriVol;
            durationSoFar += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeInSound(string soundName)
    {
        Sound s = GetSound(soundName);

        if (!s.m_Source.isPlaying)
            yield break;

        s.m_Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.m_Source.volume = 0.0f;

        float finalVolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        float durationSoFar = 0.0f;
        float totalDuration = m_FadeDuration;
        float progress = 0.0f;

        while (s.m_Source.volume < finalVolume)
        {
            progress = durationSoFar / totalDuration;
            s.m_Source.volume = (progress + Time.deltaTime / totalDuration) * finalVolume;
            durationSoFar += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }
}
