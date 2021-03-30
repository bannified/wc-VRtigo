using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    protected List<SoundData> m_Sounds;

    [SerializeField]
    private float m_FadeDuration = 2.0f;

    private List<SoundData> m_BackgroundMusics;

    #region Singleton Implementation
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
    }

    private static AudioManager CreateInstance()
    {
        GameObject go = new GameObject();
        go.name = "AudioManager";
        AudioManager audioManager = go.AddComponent<AudioManager>();
        return audioManager;
    }
    #endregion

    public static void InitAudioSourceOn(SoundData s, GameObject parent)
    {
        s.m_Source = parent.AddComponent<AudioSource>();
        s.m_Source.clip = s.GetClip();
        s.m_Source.loop = s.loop;
        s.m_Source.spatialBlend = s.spatialBlend;
    }

    public void Play(SoundData s)
    {
        InitAudioSourceOn(s, this.gameObject);

        s.m_Source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.m_Source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.m_Source.Play();

        if (!s.m_Source.loop)
            DeleteAudioSourceOnFinish(s);
    }

    public void PlayBackgroundMusics(List<SoundData> bgmSounds)
    {
        // Currently there is a background music playing
        if (m_BackgroundMusics != null && m_BackgroundMusics.Count > 1)
        {
            // Fade out the existing BGM
            foreach (SoundData bgmSound in m_BackgroundMusics)
                FadeOutAndDestroy(bgmSound);
        }

        m_BackgroundMusics = bgmSounds;

        // No new background music
        if (bgmSounds == null || bgmSounds.Count < 1)
            return;

        // Play the new background music
        foreach (SoundData bgmSound in bgmSounds)
            Play(bgmSound);
    }

    public void FadeOutAndDestroy(SoundData s)
    {
        StartCoroutine(FadeOutAndDestroyCoroutine(s));
    }

    public void FadeInSound(SoundData s)
    {
        if (s.m_Source == null)
            InitAudioSourceOn(s, this.gameObject);

        StartCoroutine(FadeInSoundCoroutine(s));
    }

    IEnumerator FadeOutAndDestroyCoroutine(SoundData s)
    {
        if (!s.m_Source.isPlaying)
            yield break;

        float oriVol = s.m_Source.volume;
        float durationSoFar = 0.0f;
        float totalDuration = m_FadeDuration;
        float progress;

        while (s.m_Source.volume > 0.0f)
        {
            progress = durationSoFar / totalDuration;
            s.m_Source.volume = (1.0f - progress - Time.deltaTime / totalDuration) * oriVol;
            durationSoFar += Time.deltaTime;

            yield return null;
        }

        s.m_Source.Stop();
        Destroy(s.m_Source);
    }

    IEnumerator FadeInSoundCoroutine(SoundData s)
    {
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

    private Coroutine DeleteAudioSourceOnFinish(SoundData s) { return StartCoroutine(DeleteAudioSourceOnFinishCoroutine(s)); }

    IEnumerator DeleteAudioSourceOnFinishCoroutine(SoundData s)
    {
        yield return new WaitForSeconds(s.GetClip().length);
        Destroy(s.m_Source);
    }
}
