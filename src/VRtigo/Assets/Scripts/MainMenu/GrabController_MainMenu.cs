using UnityEngine;
using Valve.VR;

public class GrabController_MainMenu : MonoBehaviour
{
	[SerializeField]
	protected SteamVR_Input_Sources m_HandSource = SteamVR_Input_Sources.LeftHand;

	[SerializeField]
	protected float m_GrabRadius = 0.2f;

	[SerializeField]
	protected float m_MaxAngularVelocity = 20.0f;

	[SerializeField]
	protected LayerMask m_GrabbingLayer;

	[SerializeField]
	private bool m_IsGrabbing = false;

	[SerializeField]
	private Rigidbody m_GrabbedObject;

	public void CheckForGrab(SteamVR_Action_Boolean isGrab)
	{
		m_IsGrabbing = isGrab.GetState(m_HandSource);

		if (!m_IsGrabbing)
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, m_GrabRadius, m_GrabbingLayer);

			// Get the first collider
			if (colliders.Length > 0)
			{
				m_GrabbedObject = colliders[0].transform.root.GetComponent<Rigidbody>();
				m_GrabbedObject.maxAngularVelocity = m_MaxAngularVelocity;
			}
			else
			{
				m_GrabbedObject = null;
			}
		}
		else
		{
			if (m_GrabbedObject)
			{
				// Adjust moving velocity into hand
				m_GrabbedObject.velocity = (transform.position - m_GrabbedObject.transform.position) / Time.fixedDeltaTime;

				// Follow hand rotation
				Quaternion deltaRot = transform.rotation * Quaternion.Inverse(m_GrabbedObject.transform.rotation);
				Vector3 eulerRot = new Vector3(
					Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
					Mathf.DeltaAngle(0, deltaRot.eulerAngles.y),
					Mathf.DeltaAngle(0, deltaRot.eulerAngles.z)
				);
				eulerRot *= Mathf.Deg2Rad;

				m_GrabbedObject.angularVelocity = eulerRot / Time.deltaTime;
			}
		}
	}

}
