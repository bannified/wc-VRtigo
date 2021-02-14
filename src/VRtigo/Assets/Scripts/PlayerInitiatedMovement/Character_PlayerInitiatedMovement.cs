using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, System.Flags]
public enum PlayerInitiatedMovementBitmask
{
    None = 0,
    TurnEnabled = 0x1,
    LateralMovementEnabled = 0x10,
    NonLinearMovement = 0x0100
}

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Character_PlayerInitiatedMovement : MonoBehaviour
{

    PlayerController_PlayerInitiatedMovement m_PlayerController;

    [SerializeField]
    Camera m_VRCamera;

    [SerializeField]
    protected float m_LinearMovementInputThreshold = 0.05f;

    [SerializeField]
    protected float m_MaxMoveSpeed = 200.0f;

    [SerializeField]
    protected AnimationCurve m_AccelerationCurve;

    [SerializeField]
    protected PlayerInitiatedMovementBitmask m_MovementMask = 0;

    [SerializeField]
    protected Rigidbody m_Rigidbody;

    [SerializeField]
    protected Collider m_Collider;

    [SerializeField]
    protected Vector2 m_InputDirection;

    [SerializeField]
    protected float m_InputAxisValue;

    public void PossessedBy(PlayerController_PlayerInitiatedMovement playerController)
    {
        m_PlayerController = playerController;
    }

    public void MoveForward(float axisValue)
    {
        Debug.Log("MoveForward pressed with axisValue: " + axisValue);
        m_InputAxisValue = axisValue;
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        m_InputDirection = moveDirection.normalized;
    }

    private void FixedUpdate()
    {
        Vector3 resultMoveDirection = m_VRCamera.transform.forward;
        resultMoveDirection.y = 0;
        resultMoveDirection.Normalize();

        if (HasFlagsEnabled(PlayerInitiatedMovementBitmask.LateralMovementEnabled))
        {
            resultMoveDirection = m_VRCamera.transform.forward * m_InputDirection.y + m_VRCamera.transform.right * m_InputDirection.x;
            resultMoveDirection.Normalize();
        }

        if (HasFlagsEnabled(PlayerInitiatedMovementBitmask.NonLinearMovement))
        {
            if (m_Rigidbody.velocity.magnitude > m_MaxMoveSpeed)
            {
                return;
            }

            if (m_InputAxisValue > m_LinearMovementInputThreshold)
            {
                float accel = m_AccelerationCurve.Evaluate(m_InputAxisValue);
                m_Rigidbody.velocity = m_Rigidbody.velocity + resultMoveDirection * accel * Time.fixedDeltaTime;
            } else
            {
                // [TODO]: Deceleration
                m_Rigidbody.velocity = Vector3.zero;
            }
        }
        else
        {
            if (m_InputAxisValue > m_LinearMovementInputThreshold)
            {
                m_Rigidbody.velocity = m_MaxMoveSpeed * resultMoveDirection;
            } else
            {
                m_Rigidbody.velocity = Vector3.zero;
            }
        }
    }

    bool HasFlagsEnabled(PlayerInitiatedMovementBitmask mask)
    {
        return (mask & m_MovementMask) > 0;
    }
}
