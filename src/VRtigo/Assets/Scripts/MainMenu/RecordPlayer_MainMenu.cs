using UnityEngine;
using System.Collections;

public class RecordPlayer_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected float m_MusicDurBeforeFade = 1.5f;

    [SerializeField]
    private TargetZone m_TargetZone;
    [SerializeField]
    protected GameObject m_Disc;
    [SerializeField]
    protected GameObject m_Arm;

    [SerializeField]
    protected SoundData m_ArmSound;

    [SerializeField]
    protected SoundData m_DiscSetSound;

    private SceneTransistorGrabbable_MainMenu m_CurrDisc;

    private bool m_RecordPlayerActive = false;
    private bool m_Transitioning = false;

    private float m_ArmAngle;
    private float m_DiscAngle;
    private float m_DiscSpeed;

    void Start()
    {
        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;

        AudioManager.InitAudioSourceOn(m_ArmSound, this.gameObject);
        AudioManager.InitAudioSourceOn(m_DiscSetSound, this.gameObject);
    }

    private void OnEnable()
    {
        m_TargetZone.onObjectFinishSet += OnDiscFinishSet;
    }

    private void OnDisable()
    {
        m_TargetZone.onObjectFinishSet -= OnDiscFinishSet;
    }

    private void OnDiscFinishSet(GameObject obj)
    {
        SceneTransistorGrabbable_MainMenu disc = obj.GetComponent<SceneTransistorGrabbable_MainMenu>();
        if (disc != null & !m_Transitioning && m_CurrDisc == null)
        {
            m_CurrDisc = disc;
            StartCoroutine("PlayDiscAndTransition");
        }
    }

    IEnumerator PlayDiscAndTransition()
    {
        m_Transitioning = true;

        m_CurrDisc.transform.SetParent(m_Disc.transform);
        m_CurrDisc.SetNonInteractable();

        // Play disc playing animation, and wait until animation is ready
        StartCoroutine("PlayDisc");
        yield return new WaitUntil(() => m_RecordPlayerActive);

        // Pre-play music
        AudioManager.Instance.PlayBackgroundMusics(m_CurrDisc.GetMusics());
        yield return new WaitForSeconds(m_MusicDurBeforeFade);

        // Transition
        GameManager.Instance.StartExperience(m_CurrDisc.GetExperience());
        m_Transitioning = false;
    }

    IEnumerator PlayDisc()
    {
        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;

        m_ArmSound.m_Source.Play();
        m_DiscSetSound.m_Source.Play();

        // Activate
        while (m_ArmAngle < 30.0f)
        {
            m_ArmAngle += Time.deltaTime * 30.0f;
            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscSpeed += Time.deltaTime * 80.0f;

            // Update objects
            m_Arm.transform.localEulerAngles = new Vector3(0.0f, m_ArmAngle, 0.0f);
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        m_ArmAngle = 30.0f;
        m_RecordPlayerActive = true;

        // Running
        while (m_RecordPlayerActive)
        {
            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscAngle %= 360.0f;

            // Update objects
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        // Stopping
        while (m_DiscSpeed > 0.0f || m_ArmAngle > 0.0f)
        {
            m_ArmAngle -= Time.deltaTime * 30.0f;
            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscSpeed -= Time.deltaTime * 80.0f;

            // Update objects
            m_Arm.transform.localEulerAngles = new Vector3(0.0f, m_ArmAngle, 0.0f);
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        m_ArmAngle = 0.0f;
        m_DiscSpeed = 0.0f;

        yield return null;
    }
}
