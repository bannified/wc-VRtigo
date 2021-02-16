using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController_PlayerInitiatedMovement : PlayerController
{
    [SerializeField]
    protected Character_PlayerInitiatedMovement m_CastedCharacter;

    public SteamVR_Input_Sources m_LeftHandType;
    public SteamVR_Input_Sources m_RightHandType;

    public SteamVR_Action_Boolean m_SystemMenuAction;

    /* Movement */
    public SteamVR_Action_Single m_MoveAction;
    public SteamVR_Action_Vector2 m_MoveDirection;
    public SteamVR_Action_Vector2 m_TurnDirection;

    /* Haptic */
    public SteamVR_Action_Vibration m_Haptic;

    private void SetupVRInputs()
    {
        m_SystemMenuAction.AddOnStateDownListener(SystemMenuAction_onStateDown, m_LeftHandType);
        m_SystemMenuAction.AddOnStateDownListener(SystemMenuAction_onStateDown, m_RightHandType);

        m_MoveAction.AddOnChangeListener(MoveAction_onChange, m_LeftHandType);
        m_MoveDirection.AddOnChangeListener(MoveDirection_onChange, m_LeftHandType);

        m_TurnDirection.AddOnChangeListener(m_TurnDirection_onChange, m_RightHandType);
    }

    private void TeardownVRInputs()
    {
        m_TurnDirection.RemoveOnChangeListener(m_TurnDirection_onChange, m_RightHandType);

        m_MoveAction.RemoveOnChangeListener(MoveAction_onChange, m_LeftHandType);
        m_MoveDirection.RemoveOnChangeListener(MoveDirection_onChange, m_LeftHandType);

        m_SystemMenuAction.RemoveOnStateDownListener(SystemMenuAction_onStateDown, m_LeftHandType);
    }

    private void MoveDirection_onChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        m_CastedCharacter.SetMoveDirection(axis);
    }

    private void MoveAction_onChange(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        m_CastedCharacter.MoveForward(newAxis);
    }

    private void m_TurnDirection_onChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        m_CastedCharacter.SetTurnDirection(axis);
    }

    private void SystemMenuAction_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        // [TODO]: Bring up System Menu
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CastedCharacter == null)
        {
            return;
        }

        if (SteamVR.enabled)
        {
            VRUpdate();
        } else
        {
            NonVRUpdate();
        }

        OnUpdate();
    }
    protected virtual void OnUpdate() { }

    void VRUpdate()
    {
        OnVRUpdate();
    }
    protected virtual void OnVRUpdate()
    {

    }

    void NonVRUpdate()
    {
        OnNonVRUpdate();
    }
    protected virtual void OnNonVRUpdate() 
    {
        float verticalInputAxisValue = Input.GetAxis("Vertical");
        float horizontalInputAxisValue = Input.GetAxis("Horizontal");
        float lookUpInputAxisValue = Input.GetAxis("Mouse Y");
        float lookRightInputAxisValue = Input.GetAxis("Mouse X");

        //m_Character.MoveForward(verticalInputAxisValue);

        //m_Character.MoveRight(horizontalInputAxisValue);

        if (lookUpInputAxisValue != 0.0f)
        {
            //m_Character.LookUp(lookUpInputAxisValue);
        }

        if (lookRightInputAxisValue != 0.0f)
        {
            //m_Character.LookRight(lookRightInputAxisValue);
        }
    }

    protected override void OnPossess(Character character)
    {
        Character_PlayerInitiatedMovement casted = character as Character_PlayerInitiatedMovement;
        Debug.Assert(casted != null, "PlayerController_PlayerInitiated should be Possessing a MovementCharacter_PlayerInitiatedMovement.");
        m_CastedCharacter = casted;
        SetupVRInputs();
    }

    protected override void OnUnPossess()
    {
        TeardownVRInputs();
        m_CastedCharacter = null;
    }
}
