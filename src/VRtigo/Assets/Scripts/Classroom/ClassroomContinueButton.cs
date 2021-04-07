using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRT_Constants.MainMenuConstants;

public class ClassroomContinueButton : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected SoundData m_ButtonSound;

    [SerializeField]
    protected SoundData m_ProjectorSound;

    [SerializeField]
    protected SoundData m_ProjectorWhirSound;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private float m_ButtonSpeed = 2.0f;

    [SerializeField]
    private float m_ButtonDisplacement = -0.0457f;

    [SerializeField]
    private float m_DelayFromButtonToProjector = 0.5f;

    [SerializeField]
    private bool m_IsOnCooldown = false;

    [SerializeField]
    private float m_Cooldown = 1.0f;

    [SerializeField]
    private List<UIComponent> m_ContinueButtonUIs;

    private bool m_isButtonPressed = false;
    private bool m_isEnabled = true;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_ButtonSound, this.gameObject);
        AudioManager.InitAudioSourceOn(m_ProjectorSound, this.gameObject);
        AudioManager.InitAudioSourceOn(m_ProjectorWhirSound, this.gameObject);

        bool shouldSpawnInClassroom = false;
        PersistenceManager.Instance.TryGetBool(MainMenuConstants.SPAWN_IN_CLASSROOM_BOOL, ref shouldSpawnInClassroom);

        if (shouldSpawnInClassroom)
            StartCoroutine(ClassroomSequence());
    }

    private void OnEnable()
    {
        if (ClassroomManager.Instance != null)
        {
            ClassroomManager.Instance.OnLessonEnd += ClassroomLessonEnd;
        }
    }

    private void OnDisable()
    {
        if (ClassroomManager.Instance != null)
        {
            ClassroomManager.Instance.OnLessonEnd -= ClassroomLessonEnd;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_IsOnCooldown && m_isEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            Activate();
        }
    }

    private void ClassroomLessonEnd(ClassroomLessonData classroomLessonData)
    {
        // There is no more need for continue button UIs
        for (int i = 0; i < m_ContinueButtonUIs.Count; i++)
            m_ContinueButtonUIs[i].Disable();

        // Continue button should not be able to be pressed again
        m_isEnabled = false;

        if (m_ProjectorWhirSound.m_Source.isPlaying)
            AudioManager.Instance.FadeOutAndDestroy(m_ProjectorWhirSound);
    }

    public void Activate()
    {
        if (!m_isButtonPressed && !m_IsOnCooldown)
        {
            StartCoroutine(CooldownCoroutine());
            StartCoroutine(ContinueLesson());
        }
    }

    IEnumerator ClassroomSequence()
    {
        yield return new WaitForEndOfFrame();

        // NOTE: Unable to do this via ClassroomManager.OnLessonStart, as OnLessonStart
        // is invoked before ClassroomContinueButton exist
        m_ProjectorWhirSound.m_Source.Play();

        AudioManager.Instance.PlayBackgroundMusics(Level_MainMenu.Instance.m_MainMenuBGMs);
    }

    IEnumerator CooldownCoroutine()
    {
        m_IsOnCooldown = true;
        yield return new WaitForSecondsRealtime(m_Cooldown);
        m_IsOnCooldown = false;
    }

    IEnumerator ContinueLesson()
    {
        // Play button sound
        m_ButtonSound.m_Source.Play();

        // Play animation
        StartCoroutine(PressButton(new Vector3(0, m_ButtonDisplacement, 0)));

        // Play projector sound after delay
        yield return new WaitForSeconds(m_DelayFromButtonToProjector);
        m_ProjectorSound.m_Source.Play();

        // Proceed to next step
        ClassroomManager.Instance.GoToLessonNextStep();
    }

    IEnumerator PressButton(Vector3 displacement)
    {
        m_isButtonPressed = true;

        // Press
        yield return StartCoroutine(MoveButton(displacement));

        // Back to original position
        yield return StartCoroutine(MoveButton(-displacement));

        m_isButtonPressed = false;
    }

    IEnumerator MoveButton(Vector3 displacement)
    {
        Vector3 targetPos = transform.position + displacement;
        Vector3 offset;

        while ((targetPos - transform.position).magnitude > 0.01f)
        {
            offset = (targetPos - transform.position) * Time.deltaTime * m_ButtonSpeed;
            transform.position += offset;
            yield return null;
        }
    }
}
