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
    private float m_FadeDuration = 2.0f;

    private List<string> m_BackgroundMusicNames;

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
            InitAudioSourceOn(s, this.gameObject);

            try
            {
                m_SoundsInternal.Add(s.GetName(), s);
            }
            catch
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

    public static void InitAudioSourceOn(Sound s, GameObject parent)
    {
        s.m_Source = parent.AddComponent<AudioSource>();
        s.m_Source.clip = s.GetClip();
        s.m_Source.loop = s.loop;
        s.m_Source.spatialBlend = s.spatialBlend;
    }

    public void Play(string soundName)
    {
        Sound s = GetSound(soundName);

        if (s != null)
        {
            s.m_Source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.m_Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.m_Source.Play();
        }
    }

    public void PlayBackgroundMusics(List<string> soundNames)
    {
        // Currently there is a background music playing
        if (m_BackgroundMusicNames != null && m_BackgroundMusicNames.Count > 1)
        {
            // Play the new background music
            foreach (string bgmSoundName in m_BackgroundMusicNames)
                FadeOutSoundWithName(bgmSoundName);
        }

        m_BackgroundMusicNames = soundNames;

        // No new background music
        if (soundNames == null || soundNames.Count < 1)
            return;

        // Play the new background music
        foreach (string soundName in soundNames)
            Play(soundName);
    }

    public void FadeOutSoundWithName(string soundName)
    {
        StartCoroutine("FadeOutSound", soundName);
    }

    public void FadeInSoundWithName(string soundName)
    {
        StartCoroutine("FadeInSound", soundName);
    }

    private Sound GetSound(string soundName)
    {
        if (!m_SoundsInternal.Contains(soundName))
        {
            Debug.LogError(string.Format("{0} does not exist!", soundName));
            return null;
        }
        return m_SoundsInternal[soundName] as Sound;
    }

    IEnumerator FadeOutSound(string soundName)
    {
        Sound s = GetSound(soundName);

        if (!s.m_Source.isPlaying)
            yield break;

        float oriVol = s.m_Source.volume;
        float durationSoFar = 0.0f;
        float totalDuration = m_FadeDuration;
        float progress = 0.0f;

        while (s.m_Source.volume > 0.0f)
        {
            progress = durationSoFar / totalDuration;
            s.m_Source.volume = (1.0f - progress - Time.deltaTime / totalDuration) * oriVol;
            durationSoFar += Time.deltaTime;

            yield return null;
        }

        s.m_Source.Stop();
    }

    IEnumerator FadeInSound(string soundName)
    {
        Sound s = GetSound(soundName);

        if (s == null || s.m_Source.isPlaying)
            yield break;

        s.m_Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.m_Source.volume = 0.0f;
        s.m_Source.Play();

        float finalVolume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        float durationSoFar = 0.0f;
        float totalDuration = m_FadeDuration;
        float progress = 0.0f;

        while (s.m_Source.volume < finalVolume)
        {
            progress = durationSoFar / totalDuration;
            s.m_Source.volume = (progress + Time.deltaTime / totalDuration) * finalVolume;
            durationSoFar += Time.deltaTime;

            yield return null;
        }
    }
}
