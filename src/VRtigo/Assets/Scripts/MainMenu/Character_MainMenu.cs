using UnityEngine;

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
	protected Rigidbody m_Rigidbody;

	[Header("Linear Movement Settings")]
	[SerializeField]
	protected float m_LinearMovementInputThreshold = 0.05f;

	[SerializeField]
	protected float m_MaxMoveSpeed = 200.0f;

	[Header("Turning Settings")]
	[SerializeField]
	protected float m_TurnRate = 1.0f;

	[SerializeField]
	protected float m_TurnInputThreshold = 0.1f;

	[SerializeField]
	protected Vector2 m_InputDirection;

	[SerializeField]
	protected Vector2 m_TurnInputDirection;

	[SerializeField]
	protected float m_InputAxisValue;

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

	private void FixedUpdate()
	{
		// Handle turning
		Vector3 cameraForward = m_VRCamera.transform.forward;

		if (m_TurnInputDirection.magnitude > m_TurnInputThreshold)
		{
			m_CameraRig.transform.Rotate(m_CameraRig.transform.up, m_TurnInputDirection.x * m_TurnRate * Time.fixedDeltaTime);
		}

		Vector3 resultMoveDirection = m_VRCamera.transform.forward;
		resultMoveDirection.y = 0;
		resultMoveDirection.Normalize();

		// Handle lateral movement
		resultMoveDirection = m_VRCamera.transform.forward * m_InputDirection.y + m_VRCamera.transform.right * m_InputDirection.x;
		resultMoveDirection.Normalize();

		// Linear movement
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
}
