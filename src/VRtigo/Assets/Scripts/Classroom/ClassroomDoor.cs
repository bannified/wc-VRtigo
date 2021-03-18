using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClassroomDoor : MonoBehaviour
{
    [SerializeField]
    protected Sound m_DoorLockedSound;

    [SerializeField]
    protected ExperienceData m_ExpData;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private GameObject m_DoorObject;

    [SerializeField]
    private float m_MaxDoorAngRot = 90.0f;

    [SerializeField]
    private float m_DoorSpeed = 0.8f;

    public bool hasLessonEnd = false;

    private bool isDoorClosed = true;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_DoorLockedSound, this.gameObject);
    }
    public void OpenDoor()
    {
        isDoorClosed = false;
        StartCoroutine("RotateDoor");
    }

    public void SetExp(ExperienceData expData)
    {
        m_ExpData = expData;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player can only transition to other scene when the door is closed
        if (isDoorClosed && m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            if (hasLessonEnd)
            {
                // Door is locked, player must finish the lesson
                m_DoorLockedSound.m_Source.Play();
            }
            else
            {
                // Lesson finished, go to the alternative experience
                GameManager.Instance.StartExperience(m_ExpData);
            }
        }
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
