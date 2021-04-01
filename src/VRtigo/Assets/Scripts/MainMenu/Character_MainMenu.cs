
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Character_MainMenu : Character
{
    [Header("PlayerController")]
    [SerializeField]
    protected PlayerController_MainMenu m_CastedPlayerController;

    [Header("Components")]
    [SerializeField]
    protected Camera m_VRCamera;
    [SerializeField]
    protected GameObject m_CameraRig;
    [SerializeField]
    protected GrabController_MainMenu m_LeftGrab;
    [SerializeField]
    protected GrabController_MainMenu m_RightGrab;

    [SerializeField]
    protected Rigidbody m_Rigidbody;

    [Header("Settings", order = 0)]

    [Header("Movement Settings")]
    [SerializeField]
    protected float m_LinearMovementInputThreshold = 0.05f;

    [SerializeField]
    protected float m_MaxMoveSpeed = 200.0f;

    [Header("Non-Linear Movement Settings")]

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
        Vector3 cameraForward = m_VRCamera.transform.forward;

        // Handle camera orientation
        if (m_TurnInputDirection.magnitude > m_TurnInputThreshold)
        {
            m_CameraRig.transform.Rotate(m_CameraRig.transform.up, m_TurnInputDirection.x * m_TurnRate * Time.fixedDeltaTime);
        }

        Vector3 resultMoveDirection = m_VRCamera.transform.forward;
        resultMoveDirection.y = 0;

        // Lateral movement
        resultMoveDirection = m_VRCamera.transform.forward * m_InputDirection.y + m_VRCamera.transform.right * m_InputDirection.x;
        resultMoveDirection.Normalize();

        if (m_InputAxisValue > m_LinearMovementInputThreshold)
        {
            m_Rigidbody.velocity = m_MaxMoveSpeed * resultMoveDirection;
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    protected override void OnPossessedBy(PlayerController playerController)
    {
        PlayerController_MainMenu casted = playerController as PlayerController_MainMenu;
        Debug.Assert(casted != null, "Character_MainMenu should have been Possessed by a PlayerController_MainMenu.");
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

    public void Grab(bool isLeftGrabbing, bool isRightGrabbing)
    {
        m_LeftGrab.CheckForGrab(isLeftGrabbing);
        m_RightGrab.CheckForGrab(isRightGrabbing);
    }

    // Returns an GameObject[] of [left grabbed object, right grabbed object]
    public GameObject[] GetGrabbedObjects()
    {
        return new GameObject[] { m_LeftGrab.GetGrabbedObject(), m_RightGrab.GetGrabbedObject() };
    }
}

