using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTextTrigger : MonoBehaviour, IUIComponent
{
    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    [SerializeField]
    private List<AnimatedText> m_AnimTexts;

    private bool m_IsEnabled = true;
    private bool m_IsWithinBoundary = false;

    public void SetActive()
    {
        m_IsEnabled = true;
        if (m_IsWithinBoundary)
            SetVisible();
    }

    public void SetDisable()
    {
        m_IsEnabled = false;
        if (m_IsWithinBoundary)
            SetInvisible();
    }

    public void SetVisible()
    {
        foreach (AnimatedText animText in m_AnimTexts)
            animText.TransitionIn();
    }

    public void SetInvisible()
    {
        foreach (AnimatedText animText in m_AnimTexts)
            animText.TransitionOut();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            SetVisible();

        m_IsWithinBoundary = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            SetInvisible();

        m_IsWithinBoundary = false;
    }
}
