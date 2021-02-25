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
        if (other.gameObject.tag == "Player")
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
    }
}
