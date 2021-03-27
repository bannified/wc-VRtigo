using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTextActivator : MonoBehaviour, IActivatable
{
    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    [SerializeField]
    private List<AnimatedTextActivatable> m_Activatables;

    private List<AnimatedTextActivatable> m_Activatables_Internal;

    [SerializeField]
    private bool m_Activated = false;

    private void Awake()
    {
        m_Activatables_Internal = new List<AnimatedTextActivatable>();

        foreach (AnimatedTextActivatable animTextObj in m_Activatables)
        {
            AnimatedTextActivatable animText = animTextObj as AnimatedTextActivatable;
            if (animText != null)
            {
                m_Activatables_Internal.Add(animText);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            if (!m_Activated)
            {
                Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            if (m_Activated)
            {
                Deactivate();
            }
        }
    }

    public void Activate()
    {
        m_Activated = true;
        foreach (AnimatedTextActivatable animText in m_Activatables)
        {
            animText.Activate();
        }
    }

    public void Deactivate()
    {
        m_Activated = false;
        foreach (AnimatedTextActivatable animText in m_Activatables)
        {
            animText.Deactivate();
        }
    }
}
