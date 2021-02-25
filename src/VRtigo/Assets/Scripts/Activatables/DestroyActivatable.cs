using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private GameObject m_TargetToDestroy;

    public void Activate()
    {
        Destroy(m_TargetToDestroy);
    }
}
