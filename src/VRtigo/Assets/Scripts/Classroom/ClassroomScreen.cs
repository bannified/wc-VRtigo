using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassroomScreen : MonoBehaviour
{
    [Header("Subtitles")]
    [SerializeField]
    private TMP_Text m_SubtitleText;

    private Coroutine m_DialogueRevealCoroutine;
    private Coroutine m_PostLessonCoroutine;

    [SerializeField]
    private ClassroomNarrator m_Narrator;

    [SerializeField, 
        Tooltip("The length of the lesson in seconds. After this duration, the lesson will skip to the End of the Lesson.\n" +
        "If this value is negative, the lesson will go to the end of the lesson automatically.")]
    private float m_LessonLength = 20.0f;

    [SerializeField,
    Tooltip("The duration to be elapsed post LessonLength in seconds, before the lesson goes to the next step. " +
        "After this duration, the lesson will skip to the next step.\n" +
    "If this value is negative, the lesson will not skip forward to the next step automatically.")]
    private float m_PostLessonEndAutoSkipDelay = -1.0f;

    private void Awake()
    {
        SetupListeners();
    }

    private void SetupListeners()
    {
        ClassroomManager.Instance.OnLessonStepStart += HandleOnLessonStepStart;
        ClassroomManager.Instance.OnLessonStepEnd += HandleOnLessonStepEnd;
        ClassroomManager.Instance.OnLessonStepSkip += HandleOnLessonStepSkip;
    }

    private void HandleOnLessonStepStart(LessonStep lessonStep)
    {
        m_SubtitleText.SetText(lessonStep.m_DialogueString);
        m_SubtitleText.maxVisibleCharacters = 0;

        if (m_DialogueRevealCoroutine != null)
        {
            StopCoroutine(m_DialogueRevealCoroutine);
        }

        if (m_PostLessonCoroutine != null)
        {
            StopCoroutine(m_PostLessonCoroutine);
        }

        m_DialogueRevealCoroutine = StartCoroutine(DialogueRevealRoutine());

        m_Narrator.NarrateString(m_SubtitleText.text);
    }

    private void HandleOnLessonStepEnd(LessonStep lessonStep)
    {
        RevealWholeSubtitle();

        m_PostLessonCoroutine = StartCoroutine(PostLessonRoutine());
    }

    private void HandleOnLessonStepSkip(LessonStep lessonStep)
    {
        if (m_DialogueRevealCoroutine != null)
        {
            StopCoroutine(m_DialogueRevealCoroutine);
        }

        RevealWholeSubtitle();
    }

    private void RevealWholeSubtitle()
    {
        m_SubtitleText.maxVisibleCharacters = m_SubtitleText.text.Length;
    }

    private IEnumerator DialogueRevealRoutine()
    {
        RevealWholeSubtitle();

        if (m_LessonLength >= 0.0f)
        {
            yield return new WaitForSeconds(m_LessonLength);
        }

        ClassroomManager.Instance.EndLessonStep();

        yield return null;
    }

    private IEnumerator PostLessonRoutine()
    {
        if (m_PostLessonEndAutoSkipDelay >= 0.0f)
        {
            yield return new WaitForSeconds(m_PostLessonEndAutoSkipDelay);
            ClassroomManager.Instance.NextStep();
        }

        yield return null;
    }

}
