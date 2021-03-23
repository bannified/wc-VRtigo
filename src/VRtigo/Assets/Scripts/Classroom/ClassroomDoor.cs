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
    protected ExperienceData m_ExpData;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private GameObject m_DoorObject;

    [SerializeField]
    private float m_OpenDoorAngRot = 90.0f;

    [SerializeField]
    private float m_LockedDoorAngRot = 5.0f;

    [SerializeField]
    private float m_OpenDoorSpeed = 0.8f;

    [SerializeField]
    private float m_LockedDoorSpeed = 1.3f;

    private Coroutine m_LockedDoorCoroutine;

    private float m_DoorSpeed = 0.8f;
    private float m_MaxDoorAngRot = 90.0f;

    private bool hasLessonEnd = false;
    private bool isDoorClosed = true;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_DoorLockSound, this.gameObject);
        AudioManager.InitAudioSourceOn(m_DoorOpenSound, this.gameObject);
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
        m_MaxDoorAngRot = m_OpenDoorAngRot;
        m_DoorSpeed = m_OpenDoorSpeed;
        
        StartCoroutine("RotateDoor");
    }

    public void PlayLockedDoorAnim()
    {
        m_MaxDoorAngRot = m_LockedDoorAngRot;
        m_DoorSpeed = m_LockedDoorSpeed;

        if (m_LockedDoorCoroutine != null)
            m_LockedDoorCoroutine = StartCoroutine(LockedDoorAnim());
    }

    private void ClassroomLessonEnd(ClassroomLessonData classroomLessonData)
    {
        hasLessonEnd = true;
        isDoorClosed = false;

        ExperienceData currentExperience = GameManager.Instance.GetCurrentExperience();
        if (currentExperience != null)
        {
            if (currentExperience.NextExperienceData != null)
            {
                m_ExpData = currentExperience.NextExperienceData;
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
            if (hasLessonEnd)
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

    IEnumerator LockedDoorAnim()
    {
        yield return StartCoroutine("RotateDoor");
        m_MaxDoorAngRot *= -1;

        yield return StartCoroutine("RotateDoor");
        m_MaxDoorAngRot *= -1;

        m_LockedDoorCoroutine = null;
    }

    IEnumerator RotateDoor()
    {
        float rotateSoFar = 0.0f;
        float rotationTarget = m_DoorObject.transform.rotation.y + m_MaxDoorAngRot;
        float angleToRotate;

        while (rotateSoFar < m_MaxDoorAngRot)
        {
            angleToRotate = (rotationTarget - m_DoorObject.transform.rotation.y) * Time.deltaTime * m_DoorSpeed;
            m_DoorObject.transform.Rotate(Vector3.up, -angleToRotate);

            rotateSoFar += angleToRotate;
            yield return null;
        }
    }
}
