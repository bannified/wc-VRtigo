using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayerTextTrigger_MainMenu : UIComponent
{
    [SerializeField]
    private List<SceneTransistorGrabbable_MainMenu> m_DiscsThatActivate;

    [SerializeField]
    private List<AnimatedText> m_AnimTexts;

    private bool m_IsEnabled = true;
    private bool m_IsCurrActive = false;

    private int m_DiscOnGrab = 0;

    private void OnEnable()
    {
        foreach (SceneTransistorGrabbable_MainMenu disc in m_DiscsThatActivate)
        {
            disc.onGrab += IncrDiscOnGrab;
            disc.onDrop += DecrDiscOnGrab;
        }
    }

    private void OnDisable()
    {
        foreach (SceneTransistorGrabbable_MainMenu disc in m_DiscsThatActivate)
        {
            disc.onGrab -= IncrDiscOnGrab;
            disc.onDrop -= DecrDiscOnGrab;
        }
    }

    // Wrappers
    private void IncrDiscOnGrab(Grabbable grabbable) 
    {
        m_DiscOnGrab++;
        
        // The player previously have no disc, now he has a disc
        if (m_DiscOnGrab == 1)
            SetVisible();
    }

    private void DecrDiscOnGrab(Grabbable grabbable) 
    { 
        m_DiscOnGrab--;

        // Player dropped the last disc he has
        if (m_DiscOnGrab == 0)
            SetInvisible();
    }

    public override void Enable()
    {
        m_IsEnabled = true;
    }

    public override void Disable()
    {
        if (m_IsCurrActive)
            SetInvisible();
        
        m_IsEnabled = false;
    }

    public override void SetVisible()
    {
        if (m_IsEnabled && !m_IsCurrActive)
        {
            m_IsCurrActive = true;
            foreach (AnimatedText animText in m_AnimTexts)
                animText.TransitionIn();
        }
    }

    public override void SetInvisible()
    {
        if (m_IsEnabled && m_IsCurrActive)
        {
            m_IsCurrActive = false;
            foreach (AnimatedText animText in m_AnimTexts)
                animText.TransitionOut();
        }
    }
}
