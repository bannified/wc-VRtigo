using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClassroomDoor : MonoBehaviour
{
    [SerializeField]
    protected Sound m_DoorLockSound;

    [SerializeField]
    protected Sound m_DoorOpenSound;

    [SerializeField]
    private List<BoxCollider> m_UITriggers;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private GameObject m_DoorObject;

    [SerializeField]
    private float m_OpenDoorAngRot = -90.0f;

    [SerializeField]
    private float m_LockedDoorAngRot = -0.5f;

    [SerializeField]
    private float m_OpenDoorSpeed = 0.8f;

    [SerializeField]
    private float m_LockedDoorSpeed = 7f;

    private ExperienceData m_ExpData;

    private Coroutine m_LockedDoorCoroutine;

    private float m_DoorSpeed = 0.8f;

    private bool hasLessonEnd = false;
    private bool isDoorClosed = true;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_DoorLockSound, this.gameObject);
        AudioManager.InitAudioSourceOn(m_DoorOpenSound, this.gameObject);

        SetUITriggersEnabled(false);
    }

    private void OnEnable()
    {
        ClassroomManager.Instance.OnLessonEnd += ClassroomLessonEnd;
    }

    private void OnDisable()
    {
        ClassroomManager.Instance.OnLessonEnd -= ClassroomLessonEnd;
    }

    public void PlayOpenDoorAnim()
    {
        if (isDoorClosed)
        {
            isDoorClosed = false;
            m_DoorSpeed = m_OpenDoorSpeed;
            StartCoroutine("OpenDoorAnim", m_OpenDoorAngRot);
        }
    }

    public void PlayLockedDoorAnim()
    {
        if (m_LockedDoorCoroutine == null)
        {
            m_DoorSpeed = m_LockedDoorSpeed;
            m_LockedDoorCoroutine = StartCoroutine("LockedDoorAnim", m_LockedDoorAngRot);
        }
    }

    private void ClassroomLessonEnd(ClassroomLessonData classroomLessonData)
    {
        hasLessonEnd = true;

        ExperienceData currentExperience = GameManager.Instance.GetCurrentExperience();
        if (currentExperience != null)
        {
            if (currentExperience.NextExperienceData != null)
            {
                m_ExpData = currentExperience.NextExperienceData;
                SetUITriggersEnabled(true);
            }
            else
            {
                m_DoorOpenSound.m_Source.Play();
                PlayOpenDoorAnim();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player can only transition to other scene when the door is closed
        if (isDoorClosed && m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            if (!hasLessonEnd)
            {
                // Door is locked, player must finish the lesson
                m_DoorLockSound.m_Source.Play();
                PlayLockedDoorAnim();
            }
            else
            {
                // Lesson finished, go to the alternative experience
                GameManager.Instance.StartExperience(m_ExpData);
            }
        }
    }

    private void SetUITriggersEnabled(bool val)
    {
        for (int i = 0; i < m_UITriggers.Count; i++)
            m_UITriggers[i].enabled = val;
    }

    IEnumerator OpenDoorAnim(float deltaRot)
    {
        // Delay to wait for sound effect start
        // TODO: Find better door opening SFX or cut the current one
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine("RotateDoor", deltaRot);
    }

    IEnumerator LockedDoorAnim(float deltaRot)
    {
        yield return StartCoroutine("RotateDoor", deltaRot);
        yield return StartCoroutine("RotateDoor", -deltaRot);
        m_LockedDoorCoroutine = null;
    }

    IEnumerator RotateDoor(float deltaRot)
    {
        float absDeltaRot = Mathf.Abs(deltaRot);
        float rotationTarget = m_DoorObject.transform.rotation.y + deltaRot;
        float angleRotatedSoFar = 0.0f;
        float angleToRotate;

        while (angleRotatedSoFar < absDeltaRot)
        {
            angleToRotate = (rotationTarget - m_DoorObject.transform.rotation.y) * Time.deltaTime * m_DoorSpeed;
            m_DoorObject.transform.Rotate(Vector3.up, angleToRotate);

            angleRotatedSoFar += Mathf.Abs(angleToRotate);
            yield return null;
        }

        yield return null;
    }
}