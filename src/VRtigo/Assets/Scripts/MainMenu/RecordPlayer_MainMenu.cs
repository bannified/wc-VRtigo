using UnityEngine;
using System.Collections;

public class RecordPlayer_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected bool m_RecordPlayerActive = false;
    [SerializeField]
    protected bool m_SettingDisc = false;

    [SerializeField]
    protected float m_SettingSpeed = 0.05f;
    [SerializeField]
    protected float m_SettingMaxAngularVelocity = 20.0f;

    [SerializeField]
    protected GameObject m_Disc;
    [SerializeField]
    protected GameObject m_Arm;
    [SerializeField]
    protected GameObject m_DiscTarget;
    [SerializeField]
    protected SceneTransistorGrabbable_MainMenu m_CurrDisc;

    private float m_ArmAngle;
    private float m_DiscAngle;
    private float m_DiscSpeed;

    void Start()
    {
        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;
    }
    public void Activate()
    {
        if (!m_RecordPlayerActive)
        {
            StartCoroutine("PlayDisc");
        }
    }

    public void Deactivate()
    {
        m_RecordPlayerActive = false;
    }

    public void SetDisc(SceneTransistorGrabbable_MainMenu disc)
    {
        if (!m_SettingDisc)
        {
            m_CurrDisc = disc;
            StartCoroutine("SetDiscToTarget");
        }
    }

    IEnumerator SetDiscToTarget()
    {
        m_SettingDisc = true;

        Vector3 targetPos = m_DiscTarget.transform.position;
        Quaternion targetRot = m_DiscTarget.transform.rotation;
        Rigidbody objRb = m_CurrDisc.GetComponent<Rigidbody>();

        if (objRb == null)
            yield break;

        bool isAtPosition = false;
        bool isAtRotation = false;
        objRb.maxAngularVelocity = m_SettingMaxAngularVelocity;

        while (!isAtPosition || !isAtRotation)
        {
            // Adjust moving velocity into target
            objRb.velocity = (targetPos - objRb.transform.position) / Time.fixedDeltaTime;
            objRb.velocity *= m_SettingSpeed;

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
            isAtRotation = Mathf.Abs(Quaternion.Dot(targetRot, objRb.transform.rotation) - 1.0f) < 0.01f;
            yield return new WaitForFixedUpdate();
        }

        objRb.velocity = Vector3.zero;

        m_SettingDisc = false;
    }

    IEnumerator PlayDisc()
    {
        m_RecordPlayerActive = true;

        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;

        // Activate
        while (m_ArmAngle < 30.0f)
        {
            m_ArmAngle += Time.deltaTime * 30.0f;
            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscSpeed += Time.deltaTime * 80.0f;

            // Update objects
            m_Arm.transform.localEulerAngles = new Vector3(0.0f, m_ArmAngle, 0.0f);
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        m_ArmAngle = 30.0f;

        // Running
        while (m_RecordPlayerActive)
        {
            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscAngle %= 360.0f;

            // Update objects
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        // Stopping
        while (m_DiscSpeed > 0.0f || m_ArmAngle > 0.0f)
        {
            m_ArmAngle -= Time.deltaTime * 30.0f;

            m_DiscAngle += Time.deltaTime * m_DiscSpeed;
            m_DiscSpeed -= Time.deltaTime * 80.0f;

            // Update objects
            m_Arm.transform.localEulerAngles = new Vector3(0.0f, m_ArmAngle, 0.0f);
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }

        m_ArmAngle = 0.0f;
        m_DiscSpeed = 0.0f;
    }
}
