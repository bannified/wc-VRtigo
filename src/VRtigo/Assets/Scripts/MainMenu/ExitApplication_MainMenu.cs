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

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    private SteamVR_Action_Vibration m_Haptic;
    private PlayerController_MainMenu m_PlayerController;
    private Coroutine m_ExitCoroutine;
    private int m_NumInContact = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            m_PlayerController = other.transform.root.GetComponent<PlayerController_MainMenu>();
            if (m_PlayerController != null)
                m_Haptic = m_PlayerController.m_Haptic;

            m_NumInContact++;

            // First in contact, start exit countdown
            if (m_NumInContact == 1)
                StartExitCountdown();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            m_NumInContact--;

            // Last in contact
            if (m_NumInContact == 0)
            {
                // Cancel the exit countdown when player move outside the boundary
                StopExitCountdown();

                m_Haptic = null;
                m_PlayerController = null;
            }
        }
    }

    private void StartExitCountdown()
    {
        m_ProgressTiles.SetVisible();
        m_ExitCoroutine = StartCoroutine(ExitCountdown());
    }

    private void StopExitCountdown()
    {
        m_ProgressTiles.SetInvisible();
        if (m_ExitCoroutine != null)
        {
            StopCoroutine(m_ExitCoroutine);
            m_ExitCoroutine = null;
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
            if (m_Haptic != null)
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