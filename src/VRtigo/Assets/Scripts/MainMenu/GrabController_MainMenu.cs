using UnityEngine;

public class GrabController_MainMenu : MonoBehaviour
{
    public Collider[] m_Colliders;

    [SerializeField]
    protected float m_GrabRadius = 0.3f;

    [SerializeField]
    protected float m_MaxAngularVelocity = 20.0f;

    [SerializeField]
    protected float m_GrabbingSpeed = 250.0f;

    [SerializeField]
    private bool m_IsGrabbing = false;

    [SerializeField]
    private GameObject m_GrabbedObject;

    [SerializeField]
    private Grabbable m_GrabbedObjectGrabbable;

    [SerializeField]
    private Rigidbody m_GrabbedObjectRb;

    public void CheckForGrab(bool isGrab)
    {
        m_IsGrabbing = isGrab;

        if (!m_IsGrabbing)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_GrabRadius, 1 << LayerMask.NameToLayer("Grabbable"));
            if (colliders.Length > 0)
            {
                Collider chosenCollider = colliders[0];

                m_GrabbedObject = chosenCollider.gameObject;
                m_GrabbedObjectRb = chosenCollider.transform.root.GetComponent<Rigidbody>();

                if (m_GrabbedObjectRb != null)
                {
                    m_GrabbedObjectRb.maxAngularVelocity = m_MaxAngularVelocity;
                    m_GrabbedObjectGrabbable = chosenCollider.transform.root.GetComponent<Grabbable>();

                    if (m_GrabbedObjectGrabbable != null)
                        m_GrabbedObjectGrabbable.Grabbed();
                }
            }
            else
            {
                if (m_GrabbedObjectGrabbable != null)
                    m_GrabbedObjectGrabbable.Dropped();

                m_GrabbedObject = null;
                m_GrabbedObjectGrabbable = null;
                m_GrabbedObjectRb = null;
            }
        }
        else
        {
            if (m_GrabbedObjectRb != null)
            {
                // Adjust moving velocity into hand
                m_GrabbedObjectRb.velocity = (transform.position - m_GrabbedObjectRb.transform.position) * Time.fixedDeltaTime * m_GrabbingSpeed;

                // Follow hand rotation
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(m_GrabbedObjectRb.transform.rotation);
                Vector3 eulerRot = new Vector3(
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.y),
                    Mathf.DeltaAngle(0, deltaRot.eulerAngles.z)
                );
                eulerRot *= Mathf.Deg2Rad;

                m_GrabbedObjectRb.angularVelocity = eulerRot / Time.fixedDeltaTime;
            }
        }
    }

    public GameObject GetGrabbedObject()
    {
        return m_GrabbedObject;
    }
}
