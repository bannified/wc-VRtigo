using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedChangeActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    private float m_SpeedChangeScale = 1.0f;

    public void Activate()
    {
        Character_PlayerInitiatedMovement character = GameManager.Instance.GetCharacter(0) as Character_PlayerInitiatedMovement;
        if (character != null)
        {
            character.SetMaxMoveSpeed(character.GetMaxMoveSpeed() * m_SpeedChangeScale);
        }
    }
}
