using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClassroomDoor : MonoBehaviour
{
    [SerializeField]
    protected Sound doorLockedSound;

    [SerializeField]
    protected ExperienceData m_ExpData;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    public bool hasLessonEnd = false;
    
    private bool isDoorClosed = true;

    void Start()
    {
        AudioManager.InitAudioSourceOn(doorLockedSound, this.gameObject);
    }

    public void OpenDoor()
    {
        isDoorClosed = false;
        StartCoroutine("OpenDoorAnim");
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
                doorLockedSound.m_Source.Play();
            }
            else
            {
                // Lesson finished, go to the alternative experience
                GameManager.Instance.StartExperience(m_ExpData);
            }
        }
    }

    // Door open animation
    IEnumerator OpenDoorAnim()
    {
        // TODO
        yield return null;
    }
}