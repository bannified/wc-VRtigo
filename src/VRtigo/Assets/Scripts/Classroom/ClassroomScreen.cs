using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClassroomScreen : MonoBehaviour
{
    [Header("Subtitles")]
    [SerializeField]
    private TMP_Text m_SubtitleText;

    // Subtitle Reveal Speed in characters per second
    [SerializeField]
    private float m_SubtitleRevealSpeed = 10.0f;

    private Coroutine m_DialogueRevealCoroutine;

    private void Start()
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

        m_DialogueRevealCoroutine = StartCoroutine(DialogueRevealRoutine());
    }

    private void HandleOnLessonStepEnd(LessonStep lessonStep)
    {
        m_DialogueRevealCoroutine = null;

        RevealWholeSubtitle();
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
        m_SubtitleText.maxVisibleCharacters = m_SubtitleText.GetParsedText().Length;
    }

    private IEnumerator DialogueRevealRoutine()
    {
        int parsedTextLength = m_SubtitleText.text.Length;
        Debug.Log("Dialogue Reveal Routine Started." + parsedTextLength);
        while (m_SubtitleText.maxVisibleCharacters < parsedTextLength)
        {
            ++m_SubtitleText.maxVisibleCharacters;

            yield return new WaitForSeconds(1.0f / m_SubtitleRevealSpeed);
        }

        yield return null;
    }

}