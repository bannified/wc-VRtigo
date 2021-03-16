using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRT_Constants.ExperienceConstants;

[System.Serializable, System.Flags]
public enum PlayerInitiatedMovementBitmask
{
    None = 0,
    TurnEnabled = 0x1,
    LateralMovementEnabled = 0x10,
    NonLinearMovement = 0x0100
}

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Character_PlayerInitiatedMovement : Character
{
    [Header("PlayerController")]
    [SerializeField]
    protected PlayerController_PlayerInitiatedMovement m_CastedPlayerController;

    [Header("Components")]
    [SerializeField]
    protected Camera m_VRCamera;
    [SerializeField]
    protected GameObject m_CameraRig;

    [SerializeField]
    protected Rigidbody m_Rigidbody;

    [Header("Settings", order = 0)]
    [Header("Enable/Disable Movement Features", order = 1)]
    [SerializeField]
    protected PlayerInitiatedMovementBitmask m_MovementMask = 0;

    [Header("Linear Movement Settings")]
    [SerializeField]
    protected float m_LinearMovementInputThreshold = 0.05f;

    [SerializeField]
    protected float m_MaxMoveSpeed = 200.0f;

    [Header("Non-Linear Movement Settings")]

    [SerializeField]
    protected AnimationCurve m_AccelerationCurve;

    [SerializeField]
    protected AnimationCurve m_DecelerationCurve;

    [SerializeField]
    protected float m_DecelerationMagnitudeThreshold = 0.1f;

    [SerializeField]
    protected float m_TurnRate = 1.0f;

    [SerializeField]
    protected float m_TurnInputThreshold = 0.1f;

    [Header("Turning Settings")]
    [SerializeField]
    protected Vector2 m_InputDirection;

    [SerializeField]
    protected Vector2 m_TurnInputDirection;

    [SerializeField]
    protected float m_InputAxisValue;

    [Header("Non-VR debug settings")]
    protected float m_TurnAngle = 15.0f;

    private void Start()
    {
        if (SteamVR.enabled)
        {
            VRT_Helpers.ResetHMDPosition();
        }

        LoadInitialParameters();
    }

    private void LoadInitialParameters()
    {
        PersistenceManager persistenceManager = PersistenceManager.Instance;
        if (persistenceManager == null)
        {
            return;
        }

        m_MovementMask = 0;

        bool pimTurnEnabled = false;
        persistenceManager.TryGetBool(PlayerInitiatedMovement.TURN_ENABLED_KEY_BOOL, ref pimTurnEnabled);

        if (pimTurnEnabled)
        {
            m_MovementMask |= PlayerInitiatedMovementBitmask.TurnEnabled;
        }

        bool pimNonLinearEnabled = false;
        persistenceManager.TryGetBool(PlayerInitiatedMovement.NONLINEAR_ENABLED_KEY_BOOL, ref pimNonLinearEnabled);
        if (pimNonLinearEnabled)
        {
            m_MovementMask |= PlayerInitiatedMovementBitmask.NonLinearMovement;
        }

        bool pimLateralEnabled = false;
        persistenceManager.TryGetBool(PlayerInitiatedMovement.LATERAL_ENABLED_KEY_BOOL, ref pimLateralEnabled);
        if (pimLateralEnabled)
        {
            m_MovementMask |= PlayerInitiatedMovementBitmask.LateralMovementEnabled;
        }
    }

    public void MoveForward(float axisValue)
    {
        m_InputAxisValue = axisValue;
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        m_InputDirection = moveDirection.normalized;
    }

    public void SetTurnDirection(Vector2 turnDirection)
    {
        m_TurnInputDirection = turnDirection.normalized;
    }

    public void TurnLeft()
    {
        m_CameraRig.transform.Rotate(m_CameraRig.transform.up, -m_TurnAngle);
    }

    public void TurnRight()
    {
        m_CameraRig.transform.Rotate(m_CameraRig.transform.up, m_TurnAngle);
    }


    private void FixedUpdate()
    {
        if (HasFlagsEnabled(PlayerInitiatedMovementBitmask.TurnEnabled))
        {
            Vector3 cameraForward = m_VRCamera.transform.forward;

            if (m_TurnInputDirection.magnitude > m_TurnInputThreshold)
            {
                m_CameraRig.transform.Rotate(m_CameraRig.transform.up, m_TurnInputDirection.x * m_TurnRate * Time.fixedDeltaTime);
            }
        }

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
                if (m_InputAxisValue > m_LinearMovementInputThreshold)
                {
                    return;
                } else
                {
                    Decelerate();
                    return;
                }
            }

            if (m_InputAxisValue > m_LinearMovementInputThreshold)
            {
                float accel = m_AccelerationCurve.Evaluate(m_InputAxisValue);
                m_Rigidbody.velocity = m_Rigidbody.velocity + resultMoveDirection * accel * Time.fixedDeltaTime;
            } else
            {
                Decelerate();
            }
        }
        else
        {
            if (m_InputAxisValue > m_LinearMovementInputThreshold)
            {
                if (HasFlagsEnabled(PlayerInitiatedMovementBitmask.LateralMovementEnabled))
                {
                    m_Rigidbody.velocity = m_MaxMoveSpeed * resultMoveDirection;
                } else
                { // standard movement
                    if (m_InputDirection.y > -0.3f)
                    {
                        // move forward
                        m_Rigidbody.velocity = m_MaxMoveSpeed * resultMoveDirection;
                    }
                    else
                    {
                        // move backward
                        m_Rigidbody.velocity = m_MaxMoveSpeed * -resultMoveDirection;
                    }
                }
            } else
            {
                m_Rigidbody.velocity = Vector3.zero;
            }
        }
    }

    void Decelerate()
    {
        Vector3 direction = (-m_Rigidbody.velocity).normalized;

        if (m_Rigidbody.velocity.magnitude <= m_DecelerationMagnitudeThreshold)
        {
            m_Rigidbody.velocity = Vector3.zero;
            return;
        }

        float speedRatio = m_Rigidbody.velocity.magnitude / m_MaxMoveSpeed;

        float decel = m_DecelerationCurve.Evaluate(speedRatio);
        m_Rigidbody.velocity = m_Rigidbody.velocity + direction * decel * Time.fixedDeltaTime;
    }

    bool HasFlagsEnabled(PlayerInitiatedMovementBitmask mask)
    {
        return (mask & m_MovementMask) > 0;
    }

    protected override void OnPossessedBy(PlayerController playerController)
    {
        PlayerController_PlayerInitiatedMovement casted = playerController as PlayerController_PlayerInitiatedMovement;
        Debug.Assert(casted != null, "Character_PlayerIninitiatedMovement should have been Possessed by a PlayerController_PlayerInitiatedMovement.");
        m_CastedPlayerController = casted;
    }

    public float GetMaxMoveSpeed()
    {
        return m_MaxMoveSpeed;
    }

    public void SetMaxMoveSpeed(float maxMoveSpeed)
    {
        m_MaxMoveSpeed = maxMoveSpeed;
    }
}
