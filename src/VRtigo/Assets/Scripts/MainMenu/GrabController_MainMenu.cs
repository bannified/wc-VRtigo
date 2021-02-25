using UnityEngine;
using Valve.VR;

public class GrabController_MainMenu : MonoBehaviour
{
    public Collider[] m_Colliders;

    [SerializeField]
    protected SteamVR_Input_Sources m_HandSource = SteamVR_Input_Sources.LeftHand;

    [SerializeField]
    protected float m_GrabDistance = 0.5f;

    [SerializeField]
    protected float m_MaxAngularVelocity = 20.0f;

    [SerializeField]
    protected LayerMask m_GrabbingLayer;

    [SerializeField]
    private bool m_IsGrabbing = false;

    [SerializeField]
    private IGrabbable m_GrabbedObjectGrabbable;

    [SerializeField]
    private Rigidbody m_GrabbedObjectRb;

    public void CheckForGrab(SteamVR_Action_Boolean isGrab)
    {
        m_IsGrabbing = isGrab.GetState(m_HandSource);

        if (!m_IsGrabbing)
        {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit, m_GrabDistance);

            if (bHit)
            {
                m_GrabbedObjectRb = hit.collider.transform.root.GetComponent<Rigidbody>();
                m_GrabbedObjectRb.maxAngularVelocity = m_MaxAngularVelocity;

                m_GrabbedObjectGrabbable = hit.collider.transform.root.GetComponent<IGrabbable>();
                if (m_GrabbedObjectGrabbable != null)
                {
                    m_GrabbedObjectGrabbable.Grabbed();
                }
            }
            else
            {
                if (m_GrabbedObjectGrabbable != null)
                {
                    m_GrabbedObjectGrabbable.Dropped();
                }

                m_GrabbedObjectGrabbable = null;
                m_GrabbedObjectRb = null;
            }
        }
        else
        {
            if (m_GrabbedObjectRb != null)
            {
                // Adjust moving velocity into hand
                m_GrabbedObjectRb.velocity = (transform.position - m_GrabbedObjectRb.transform.position) / Time.fixedDeltaTime;

                // Follow hand rotation
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(m_GrabbedObjectRb.transform.rotation);
                Vector3 eulerRot = new Vector3(
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.y),
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.z)
                );
                eulerRot *= Mathf.Deg2Rad;

                m_GrabbedObjectRb.angularVelocity = eulerRot / Time.deltaTime;
            }
        }
    }

}
