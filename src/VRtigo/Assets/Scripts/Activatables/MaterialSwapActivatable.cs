using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private MeshRenderer m_TargetMeshRenderer;

    [SerializeField]
    List<Material> m_MaterialsSwapTo;

    public void Activate()
    {
        if (m_TargetMeshRenderer == null)
        {
            return;
        }

        m_TargetMeshRenderer.materials = m_MaterialsSwapTo.ToArray();
    }
}
