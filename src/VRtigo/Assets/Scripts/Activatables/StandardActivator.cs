using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardActivator : MonoBehaviour, IActivatable
{
    [SerializeField]
    private List<MonoBehaviour> m_Activatables;

    private List<IActivatable> m_Activatables_Internal;

    [SerializeField]
    private bool m_Activated = false;

    [SerializeField]
    private bool m_Reactivatable = false;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    private void Awake()
    {
        m_Activatables_Internal = new List<IActivatable>();

        foreach (MonoBehaviour mb in m_Activatables)
        {
            IActivatable activatable = mb as IActivatable;
            if (activatable != null)
            {
                m_Activatables_Internal.Add(activatable);
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

    public void Activate()
    {
        m_Activated = true;
        foreach (IActivatable activatable in m_Activatables)
        {
            activatable.Activate();
        }

        if (m_Reactivatable)
        {
            m_Activated = false;
        }
    }
}
