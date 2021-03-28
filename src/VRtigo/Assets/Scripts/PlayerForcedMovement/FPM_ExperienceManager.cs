using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRT_Constants.ExperienceConstants;

public class FPM_ExperienceManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private SplineWalker m_PlayerSplineWalker;
    [SerializeField]
    private GameObject m_PlayerCarMesh;

    [Header("Track Container Objects")]
    [SerializeField]
    private GameObject m_GentleTurnContainer;
    [SerializeField]
    private GameObject m_SharpTurnContainer;

    [Header("Spline Paths")]
    [SerializeField]
    private BezierSpline m_GentleTurnTrack;
    [SerializeField]
    private BezierSpline m_SharpTurnTrack;

    // Start is called before the first frame update
    void Start()
    {
        LoadInitialParameters();
    }

    private void LoadInitialParameters()
    {
        PersistenceManager persistenceManager = PersistenceManager.Instance;
        if (persistenceManager == null)
        {
            return;
        }

        bool pfmGentleTurns = false;
        persistenceManager.TryGetBool(PlayerForcedMovement.GENTLE_TURNS_BOOL, ref pfmGentleTurns);

        if (pfmGentleTurns)
        {
            m_GentleTurnContainer.SetActive(true);
            m_SharpTurnContainer.SetActive(false);
            m_PlayerSplineWalker.spline = m_GentleTurnTrack;
        }
        else
        {
            m_GentleTurnContainer.SetActive(false);
            m_SharpTurnContainer.SetActive(true);
            m_PlayerSplineWalker.spline = m_SharpTurnTrack;
        }

        bool pfmFrameOfRef = false;
        persistenceManager.TryGetBool(PlayerForcedMovement.FRAME_OF_REF_BOOL, ref pfmFrameOfRef);
        m_PlayerCarMesh.SetActive(pfmFrameOfRef);
    }
}
