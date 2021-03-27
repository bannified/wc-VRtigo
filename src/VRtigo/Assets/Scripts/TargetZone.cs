using System.Collections;
using UnityEngine;

public class TargetZone : MonoBehaviour
{
    public System.Action<GameObject> onObjectStartSet; // Invoked when an object need to be set
    public System.Action<GameObject> onObjectFinishSet; // Invoked when an object has done being set

    public Transform m_TargetTransform;

    private GameObject m_CurrObj = null;
    private bool m_IsSetting = false;

    [SerializeField]
    private float m_SettingMaxAngularVelocity = 20.0f;

    [SerializeField]
    private float m_SettingSpeed = 150.0f;

    private void OnTriggerStay(Collider other)
    {
        Grabbable grabbableObj = other.GetComponent<Grabbable>();

        // Only grabbable object is able to be dropped to target zone
        bool isNotGrabbed = grabbableObj != null && !grabbableObj.GetIsGrabbed();

        // Must currently not be setting and target zone is empty
        if (isNotGrabbed && !m_IsSetting && m_CurrObj == null)
        {
            grabbableObj.SetNonInteractable();

            onObjectStartSet?.Invoke(grabbableObj.gameObject);

            StartCoroutine("SetObjToTarget", grabbableObj);
        }
    }

    IEnumerator SetObjToTarget(Grabbable grabbableObj)
    {
        m_IsSetting = true;

        Vector3 targetPos = m_TargetTransform.transform.position;
        Quaternion targetRot = m_TargetTransform.transform.rotation;
        Rigidbody objRb = grabbableObj.GetComponent<Rigidbody>();

        if (objRb == null)
            yield break;

        bool isAtPosition = false;
        bool isAtRotation = false;

        // Item on target zone is not affected by gravity
        objRb.useGravity = false;
        objRb.maxAngularVelocity = m_SettingMaxAngularVelocity;

        while (!isAtPosition || !isAtRotation)
        {
            // Adjust moving velocity into target
            objRb.velocity = (targetPos - objRb.transform.position) * Time.fixedDeltaTime * m_SettingSpeed;

            // Follow target rotation
            Quaternion deltaRot = targetRot * Quaternion.Inverse(objRb.transform.rotation);
            Vector3 eulerRot = new Vector3(
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.y),
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.z)
            );
            eulerRot *= Mathf.Deg2Rad;

            objRb.angularVelocity = eulerRot / Time.fixedDeltaTime;

            isAtPosition = (targetPos - objRb.transform.position).magnitude < 0.01f;
            isAtRotation = Mathf.Approximately(Mathf.Abs(Quaternion.Dot(targetRot, objRb.transform.rotation)), 1.0f);

            yield return new WaitForFixedUpdate();
        }

        objRb.velocity = Vector3.zero;

        // Clean up
        grabbableObj.SetInteractable();
        m_IsSetting = false;

        // Signal action
        onObjectFinishSet?.Invoke(grabbableObj.gameObject);

        yield return null;
    }
}
