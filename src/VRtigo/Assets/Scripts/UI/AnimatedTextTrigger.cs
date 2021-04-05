using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTextTrigger : UIComponent
{
    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    [SerializeField]
    private List<AnimatedText> m_AnimTexts;

    private bool m_IsEnabled = true;
    private bool m_IsWithinBoundary = false;

    public override void Enable()
    {
        m_IsEnabled = true;
        if (m_IsWithinBoundary)
            SetVisible();
    }

    public override void Disable()
    {
        if (m_IsWithinBoundary)
            SetInvisible();
        m_IsEnabled = false;
    }

    public override void SetVisible()
    {
        foreach (AnimatedText animText in m_AnimTexts)
            animText.TransitionIn();
    }

    public override void SetInvisible()
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
