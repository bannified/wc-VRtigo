using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected Character m_StartingCharacter;

    [SerializeField]
    protected Character m_Character;

    /// <summary>
    /// Start implementation for PlayerController. Registers itself with the GameManager
    /// WARNING: DO NOT OVERRIDE. ie. DO NOT DECLARE AN Start() METHOD IN ITS CHILDREN CLASSES
    /// OTHERWISE YOU'LL GET 10000 YEARS OF BAD LUCK jk but
    /// !!!!! YOU HAVE BEEN WARNED !!!!!
    /// </summary>
    private void Start()
    {
        GameManager.Instance.RegisterPlayerController(this);
        if (m_StartingCharacter)
        {
            Possess(m_StartingCharacter);
        }

        OnStart();
    }
    protected void OnStart() { }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UnregisterPlayerController(this);
            OnPlayerControllerDestroyed();
        }
    }
    protected void OnPlayerControllerDestroyed() { }

    protected void Possess(Character character)
    {
        if (m_Character != null)
        {
            UnPossess();
        }

        GameManager.Instance.RegisterCharacterToController(this, character);
        character.PossessedBy(this);
        OnPossess(character);
    }
    protected virtual void OnPossess(Character character) { }

    protected void UnPossess()
    {
        GameManager.Instance.UnregisterCharacterFromController(this, m_Character);
        m_Character.UnPossessed();
        OnUnPossess();
        m_Character = null;
    }
    protected virtual void OnUnPossess() { }
}
