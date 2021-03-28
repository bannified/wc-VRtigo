using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomContinueButton : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected Sound m_ButtonSound;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private float m_ButtonSpeed = 2.0f;

    [SerializeField]
    private float m_ButtonDisplacement = -0.0457f;

    [SerializeField]
    private List<UIComponent> m_ContinueButtonUIs;

    private bool m_isButtonPressed = false;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_ButtonSound, this.gameObject);
    }

    private void OnEnable()
    {
        ClassroomManager.Instance.OnLessonEnd += ClassroomLessonEnd;
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
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            Activate();
        }
    }

    private void ClassroomLessonEnd(ClassroomLessonData classroomLessonData)
    {
        // There is no more need for continue button
        for (int i = 0; i < m_ContinueButtonUIs.Count; i++)
            m_ContinueButtonUIs[i].SetDisable();
    }

    public void Activate()
    {
        if (!m_isButtonPressed)
        {
            // Play button sound
            m_ButtonSound.m_Source.Play();

            // Play animation
            StartCoroutine("PressButton", new Vector3(0, m_ButtonDisplacement, 0));

            // Proceed to next step
            ClassroomManager.Instance.GoToLessonNextStep();
        }
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
