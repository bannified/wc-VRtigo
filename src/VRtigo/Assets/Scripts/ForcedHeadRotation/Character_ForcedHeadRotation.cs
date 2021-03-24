using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SpatialTracking;
using VRT_Constants.ExperienceConstants;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Character_ForcedHeadRotation : Character
{
    [Header("PlayerController")]
    [SerializeField]
    protected PlayerController_ForcedHeadRotation m_CastedPlayerController;

    [Header("Components")]
    [SerializeField]
    protected Camera m_VRCamera;
    [SerializeField]
    protected Animator m_CameraRootAnimator;
    [SerializeField]
    protected TrackedPoseDriver m_CameraTrackedPoseDriver;
    [SerializeField]
    protected Animator m_BlackScreenAnimator;

    [SerializeField]
    protected Rigidbody m_Rigidbody;

    [Header("Linear Movement Settings")]
    [SerializeField]
    protected float m_LinearMovementInputThreshold = 0.05f;

    [SerializeField]
    protected float m_MaxMoveSpeed = 200.0f;

    [SerializeField]
    protected Vector3 m_MoveDirection = new Vector3(0.0f, 0.0f, 1.0f);

    [Header("Turning Settings")]
    [SerializeField]
    protected Vector2 m_InputDirection;

    [SerializeField]
    protected float m_InputAxisValue;

    [SerializeField]
    protected bool m_CameraRotLocked = false;

    [Header("Walk Point")]
    protected WalkPoint m_LastWalkPoint;

    private void Start()
    {
        m_CameraTrackedPoseDriver = m_VRCamera.GetComponent<TrackedPoseDriver>();

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

        persistenceManager.TryGetBool(PlayerInitiatedMovement.TURN_ENABLED_KEY_BOOL, ref m_CameraRotLocked);

        if (!m_CameraRotLocked) // Default Implementation
        {
            m_CameraTrackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
        else // FixedCamera
        {
            m_CameraTrackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.PositionOnly;
        }
    }

    public void TurnHeadRight()
    {
        if (!m_CameraRotLocked)
        {
            return;
        }


    }

    public void TurnHeadLeft()
    {
        if (!m_CameraRotLocked)
        {
            return;
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

    private void FixedUpdate()
    {
        if (Mathf.Abs(m_InputAxisValue) > m_LinearMovementInputThreshold)
        {
            m_Rigidbody.velocity = new Vector3(0.0f, 0.0f, m_MaxMoveSpeed * m_InputDirection.y);
        }
        else
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }

    protected override void OnPossessedBy(PlayerController playerController)
    {
        PlayerController_ForcedHeadRotation casted = playerController as PlayerController_ForcedHeadRotation;
        Debug.Assert(casted != null, "Character_ForcedHeadRotation should have been Possessed by a PlayerController_ForcedHeadRotation.");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            StartRespawn();
            return;
        }

        WalkPoint walkPoint = other.gameObject.GetComponent<WalkPoint>();
        if (walkPoint != null)
        {
            m_LastWalkPoint = walkPoint;
        }
    }

    void StartRespawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        m_BlackScreenAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.833f);

        transform.position = m_LastWalkPoint.transform.position;

        m_BlackScreenAnimator.SetTrigger("FadeIn");
    }
}