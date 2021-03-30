using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlayerTextTrigger_MainMenu : UIComponent
{
    [SerializeField]
    private List<SceneTransistorGrabbable_MainMenu> m_DiscsThatActivate;

    [SerializeField]
    private List<AnimatedText> m_AnimTexts;

    [SerializeField]
    private List<UIComponent> m_RecordPlayerUIs;

    private bool m_IsEnabled = true;
    private bool m_IsCurrActive = false;

    private int m_DiscOnGrab = 0;

    private void Start()
    {
        // Start as disabled by default, until player picks up a disc
        for (int i = 0; i < m_RecordPlayerUIs.Count; i++)
        {
            m_RecordPlayerUIs[i].Disable();
        }    
    }

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

    private void IncrDiscOnGrab(Grabbable grabbable) 
    {
        m_DiscOnGrab++;
        
        // The player previously have no disc, now he has a disc
        if (m_DiscOnGrab == 1)
        {
            // Text is always visible as long as player has at least a disc
            SetVisible();

            // Also enable necessary UIs
            for (int i = 0; i < m_RecordPlayerUIs.Count; i++)
                m_RecordPlayerUIs[i].Enable();
        }
    }

    private void DecrDiscOnGrab(Grabbable grabbable) 
    { 
        m_DiscOnGrab--;

        // Player dropped the last disc he has
        if (m_DiscOnGrab == 0)
        {
            // Text is always invisible as long as player has no disc
            SetInvisible();

            // Also disable necessary UIs
            for (int i = 0; i < m_RecordPlayerUIs.Count; i++)
                m_RecordPlayerUIs[i].Disable();
        }
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
