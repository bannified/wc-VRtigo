using UnityEngine;
using System.Collections;

public class RecordPlayer_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected SceneTransistor m_SceneTransistor;
    [SerializeField]
    protected float m_MusicDelay = 0.5f;
    [SerializeField]
    protected float m_MusicDurBeforeFade = 1.5f;

    [SerializeField]
    protected float m_SettingSpeed = 250.0f;
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

    private bool m_RecordPlayerActive = false;
    private bool m_SettingDisc = false;
    private bool m_Transitioning = false;

    private float m_ArmAngle;
    private float m_DiscAngle;
    private float m_DiscSpeed;

    void Start()
    {
        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;
    }

    public bool SetDisc(SceneTransistorGrabbable_MainMenu disc)
    {
        if (!m_Transitioning)
        {
            m_CurrDisc = disc;
            StartCoroutine("SetDiscAndTransition");
            return true;
        }

        return false;
    }

    public void DeactivateRecordPlayer()
    {
        m_RecordPlayerActive = false;
    }

    IEnumerator SetDiscAndTransition()
    {
        m_Transitioning = true;

        if (!m_SettingDisc)
            yield return StartCoroutine("SetDiscToTarget");

        if (!m_RecordPlayerActive)
            StartCoroutine("PlayDisc");

        yield return new WaitForSeconds(m_MusicDelay);

        AudioManager.Instance.PlayBackgroundMusic(m_CurrDisc.GetMusicName());

        yield return new WaitForSeconds(m_MusicDurBeforeFade);

        m_SceneTransistor.FadeToScene(m_CurrDisc.GetSceneName());
        m_Transitioning = false;
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
            isAtRotation = Mathf.Abs(Quaternion.Dot(targetRot, objRb.transform.rotation) - 1.0f) < 0.01f;
            yield return new WaitForFixedUpdate();
        }

        objRb.velocity = Vector3.zero;

        m_SettingDisc = false;

        yield return null;
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

        yield return null;
    }
}
