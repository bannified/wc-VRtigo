using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected PlayerController m_PlayerController;

    public void PossessedBy(PlayerController playerController)
    {
        m_PlayerController = playerController;
        OnPossessedBy(playerController);
    }
    protected virtual void OnPossessedBy(PlayerController playerController) {}

    public void UnPossessed()
    {
        OnUnPossessed();
        m_PlayerController = null;
    }

    protected virtual void OnUnPossessed() {}

    private void OnDestroy()
    {
    }
}
