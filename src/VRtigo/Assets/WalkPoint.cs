using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class WalkPoint : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    protected WalkPoint m_NextWalkPoint;

    [SerializeField]
    protected bool m_ActivateOnStart = false;

    [SerializeField]
    protected bool m_ShowDirectionalArrow = false;

    [SerializeField]
    protected bool m_ShowGuidingLine = false;

    [Header("Components")]
    [SerializeField]
    protected GameObject m_TargetVisuals;
    [SerializeField]
    protected GameObject m_NextPointIndicator;
    [SerializeField]
    protected LineRenderer m_GuidingLine;

    [Header("Runtime Properties")]
    [SerializeField]
    protected WalkPoint m_PreviousWalkPoint;

    [SerializeField]
    protected bool m_IsActive = false;

    [SerializeField]
    protected bool m_HasBeenTriggered = false;

    [SerializeField]
    protected SteamVR_Action_Vibration m_Haptic;

    private void Start()
    {
        if (m_ActivateOnStart)
        {
            Activate();
        } else
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        m_GuidingLine.gameObject.SetActive(false);
        m_NextPointIndicator.SetActive(false);
        m_TargetVisuals.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Activate(WalkPoint walkPoint = null)
    {
        gameObject.SetActive(true);

        m_PreviousWalkPoint = walkPoint;
        m_IsActive = true;

        m_HasBeenTriggered = false;

        m_TargetVisuals.SetActive(true);
    }

    private void RevealNextWalkPoint()
    {
        if (m_ShowDirectionalArrow)
        {
            Vector3 lookAtPosition = m_NextWalkPoint.transform.position;

            Debug.Assert(m_NextPointIndicator != null, "NextPointIndicator shouldn't be null. Check if it's assigned in the Monobehavior!");
            lookAtPosition.y = m_NextPointIndicator.transform.position.y;

            m_NextPointIndicator.transform.LookAt(lookAtPosition);
            m_NextPointIndicator.SetActive(true);
        } else
        {
            m_NextPointIndicator.SetActive(false);
        }


        if (m_ShowGuidingLine)
        {
            Debug.Assert(m_GuidingLine != null, "GuidingLine shouldn't be null. Check if it's assigned in the Monobehavior!");
            m_GuidingLine.SetPosition(1, gameObject.transform.position - m_NextWalkPoint.transform.position);
            m_GuidingLine.gameObject.SetActive(true);
        } else
        {
            m_GuidingLine.gameObject.SetActive(false);
        }

        m_TargetVisuals.SetActive(false);

        m_NextWalkPoint.Activate(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == VRT_Constants.Tags.PLAYER)
        {
            if (m_HasBeenTriggered || !m_IsActive)
            {
                return;
            }

            if (m_PreviousWalkPoint != null)
            {
                m_PreviousWalkPoint.Deactivate();
            }

            if (m_NextWalkPoint != null)
            {
                RevealNextWalkPoint();
            }

            m_HasBeenTriggered = true;

            if (SteamVR.enabled)
            {
                m_Haptic.Execute(0.0f, 0.1f, 2.0f, 1.0f, SteamVR_Input_Sources.Any);
            } else
            {
                Debug.Log("Attempt to execute Haptic feedback, but ignored because SteamVR isn't enabled.");
            }
        }
    }
}
