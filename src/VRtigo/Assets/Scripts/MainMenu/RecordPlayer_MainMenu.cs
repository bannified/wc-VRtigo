using UnityEngine;
using System.Collections;

public class RecordPlayer_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected bool m_RecordPlayerActive = false;
    [SerializeField]
    protected bool m_SettingDisc = false;

    [SerializeField]
    protected GameObject m_Disc;
    [SerializeField]
    protected GameObject m_Arm;
    [SerializeField]
    protected GameObject m_DiscTarget;
    [SerializeField]
    protected SceneTransistorGrabbable m_CurrDisc;

    private int m_Mode;
    private float m_ArmAngle;
    private float m_DiscAngle;
    private float m_DiscSpeed;

    void Start()
    {
        m_Mode = 0;
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

    public void SetDisc(SceneTransistorGrabbable disc)
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

        while (!isAtPosition && !isAtRotation)
        {
            // Adjust moving velocity into hand
            objRb.velocity = (targetPos - objRb.transform.position) / Time.fixedDeltaTime;

            // Follow hand rotation
            Quaternion deltaRot = targetRot * Quaternion.Inverse(objRb.transform.rotation);
            Vector3 eulerRot = new Vector3(
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.x),
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.y),
                Mathf.DeltaAngle(0, deltaRot.eulerAngles.z)
            );
            eulerRot *= Mathf.Deg2Rad;

            objRb.angularVelocity = eulerRot / Time.deltaTime;

            isAtPosition = Mathf.Approximately((targetPos - objRb.transform.position).magnitude, 0.0f);
            isAtRotation = Mathf.Approximately(Mathf.Abs(Quaternion.Dot(targetRot, objRb.transform.rotation)), 1.0f);

            yield return new WaitForEndOfFrame();
        }

        m_SettingDisc = false;
    }

    IEnumerator PlayDisc()
    {
        m_RecordPlayerActive = true;

        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;
        m_Mode = 1;

        while (m_Mode != 0)
        {
            // Mode 1: activation
            if (m_Mode == 1)
            {
                if (m_RecordPlayerActive == true)
                {
                    m_ArmAngle += Time.deltaTime * 30.0f;
                    if (m_ArmAngle >= 30.0f)
                    {
                        m_ArmAngle = 30.0f;
                        m_Mode = 2;
                    }
                    m_DiscAngle += Time.deltaTime * m_DiscSpeed;
                    m_DiscSpeed += Time.deltaTime * 80.0f;
                }
                else
                    m_Mode = 3;
            }
            // Mode 2: running
            else if (m_Mode == 2)
            {
                if (m_RecordPlayerActive == true)
                {
                    m_DiscAngle += Time.deltaTime * m_DiscSpeed;
                    m_DiscAngle %= 360.0f;
                }
                else
                {
                    m_Mode = 3;
                }
            }
            // Mode 3: stopping
            else
            {
                if (m_RecordPlayerActive == false)
                {
                    m_ArmAngle -= Time.deltaTime * 30.0f;
                    if (m_ArmAngle <= 0.0f)
                        m_ArmAngle = 0.0f;

                    m_DiscAngle += Time.deltaTime * m_DiscSpeed;
                    m_DiscSpeed -= Time.deltaTime * 80.0f;
                    if (m_DiscSpeed <= 0.0f)
                        m_DiscSpeed = 0.0f;

                    if ((m_DiscSpeed == 0.0f) && (m_ArmAngle == 0.0f))
                        m_Mode = 0;
                }
                else
                    m_Mode = 1;
            }

            // Update objects
            m_Arm.transform.localEulerAngles = new Vector3(0.0f, m_ArmAngle, 0.0f);
            m_Disc.transform.localEulerAngles = new Vector3(0.0f, m_DiscAngle, 0.0f);

            yield return new WaitForEndOfFrame();
        }
    }
}
