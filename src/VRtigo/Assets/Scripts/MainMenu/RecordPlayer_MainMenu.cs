using UnityEngine;
using System.Collections;

public class RecordPlayer_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected bool m_RecordPlayerActive = false;

    [SerializeField]
    protected GameObject m_Disc;
    [SerializeField]
    protected GameObject m_Arm;

    protected int m_Mode;
    [SerializeField]
    protected float m_ArmAngle;
    [SerializeField]
    protected float m_DiscAngle;
    [SerializeField]
    protected float m_DiscSpeed;

    public bool start = false;

    void Start()
    {
        m_Mode = 0;
        m_ArmAngle = 0.0f;
        m_DiscAngle = 0.0f;
        m_DiscSpeed = 0.0f;
    }

    private void Update()
    {
        if (start)
            Activate();
    }

    public void Activate()
    {
        if (!m_RecordPlayerActive)
        {
            Debug.Log("PLAYING");
            StartCoroutine("PlayDisc");
        }
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
                    m_DiscAngle += Time.deltaTime * m_DiscSpeed;
                else
                    m_Mode = 3;
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
