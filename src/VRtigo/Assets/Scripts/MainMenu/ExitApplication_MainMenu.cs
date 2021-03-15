using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ExitApplication_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected float m_ExitDuration = 3.0f;

    [SerializeField]
    protected ProgressTiles m_ProgressTiles;

    private SteamVR_Action_Boolean m_ExitAction;
    private SteamVR_Action_Vibration m_Haptic;
    private PlayerController_MainMenu m_PlayerController;

    private Coroutine m_ExitCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_PlayerController = other.gameObject.GetComponent<PlayerController_MainMenu>();
            if (m_PlayerController != null)
            {
                m_ExitAction = m_PlayerController.m_ExitAppAction;
                m_Haptic = m_PlayerController.m_Haptic;

                m_ExitAction.AddOnStateDownListener(StartExitCountdown, m_PlayerController.m_LeftHandType);
                m_ExitAction.AddOnStateUpListener(StopExitCountdown, m_PlayerController.m_LeftHandType);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (m_PlayerController != null && m_ExitAction != null)
            {
                // Regardless whether button is paused or not, cancel the exit countdown when player move outside the boundary
                StopExitCountdown(m_ExitAction, m_PlayerController.m_RightHandType);

                m_ExitAction.RemoveOnStateDownListener(StartExitCountdown, m_PlayerController.m_LeftHandType);
                m_ExitAction.RemoveOnStateDownListener(StopExitCountdown, m_PlayerController.m_LeftHandType);

                m_ExitAction = null;
                m_Haptic = null;
                m_PlayerController = null;
            }
        }
    }

    private void StartExitCountdown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        m_ProgressTiles.StartProgress();
        m_ExitCoroutine = StartCoroutine(ExitCountdown());
    }

    private void StopExitCountdown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        m_ProgressTiles.CancelProgress();
        if (m_ExitCoroutine != null)
        {
            StopCoroutine(m_ExitCoroutine);
        }
    }

    IEnumerator ExitCountdown()
    {
        float durationSoFar = 0.0f;

        while (durationSoFar < m_ExitDuration)
        {
            // Update progress tiles
            m_ProgressTiles.SetProgress(durationSoFar / m_ExitDuration);

            // Add haptic feedback
            m_Haptic.Execute(0.0f, Time.deltaTime, 5.0f, 0.1f, SteamVR_Input_Sources.Any);

            durationSoFar += Time.deltaTime;
            yield return null;
        }

        m_ProgressTiles.SetProgress(durationSoFar / m_ExitDuration);

        // Delay quit so the progress tiles is fully filled before quitting
        yield return new WaitForSeconds(0.5f);

        Application.Quit(0);
    }
}