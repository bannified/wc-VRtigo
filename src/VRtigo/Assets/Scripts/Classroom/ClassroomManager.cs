using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRT_Constants.MainMenuConstants;

public class ClassroomManager : MonoBehaviour
{
    #region Singleton Implementation
    private static object _lock = new object();
    private static ClassroomManager _instance;
    public static ClassroomManager Instance
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return null;
#endif

            lock (_lock)
            {
                return _instance;
            }

        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
    #endregion

    [SerializeField]
    private ClassroomLessonData m_ClassroomLessonData;

    [SerializeField]
    private bool m_AutoplayClassroom = false;

    [Header("Runtime Classroom Data")]
    [SerializeField]
    private int m_CurrentDialogueIndex = 0;

    [SerializeField]
    private bool m_IsLessonStepCompleted = false;

    [Header("Static data (MUST BE ASSIGNED)")]
    [SerializeField]
    private Transform m_PlayerSpawnPoint;

    /// <summary>
    /// Classroom Events
    /// </summary>
    public System.Action<ClassroomLessonData> OnLessonStart;
    public System.Action<LessonStep> OnLessonStepStart;
    public System.Action<LessonStep> OnLessonStepSkip;
    public System.Action<LessonStep> OnLessonStepEnd;
    public System.Action<ClassroomLessonData> OnLessonEnd;

    public Transform GetPlayerSpawnPoint()
    {
        return m_PlayerSpawnPoint;
    }

    public void StartLesson(ClassroomLessonData classroomLessonData)
    {
        if (classroomLessonData == null)
        {
            return;
        }

        m_ClassroomLessonData = classroomLessonData;
        StartLesson();
    }

    private void StartLesson()
    {
        m_CurrentDialogueIndex = -1;
        OnLessonStart?.Invoke(m_ClassroomLessonData);
        GoToLessonNextStep();
    }

    private void Start()
    {
        TrySpawnPlayerAtClassroom();

        if (SteamVR.active)
        {
            VRT_Helpers.ResetHMDPosition();
        }

        if (m_AutoplayClassroom && GameManager.Instance != null)
        {
            ExperienceData experience = GameManager.Instance.GetCurrentExperience();
            if (experience != null) 
            {
                StartLesson(GameManager.Instance.GetCurrentExperience().LessonData);
            }
        }

        OnLessonEnd += OnLessonEnded;
    }

    private void TrySpawnPlayerAtClassroom()
    {
        bool shouldSpawnInClassroom = false;
        PersistenceManager.Instance.TryGetBool(MainMenuConstants.SPAWN_IN_CLASSROOM_BOOL, ref shouldSpawnInClassroom);

        if (shouldSpawnInClassroom)
        {
            Character character = GameManager.Instance.GetCharacter(0);
            character.transform.position = m_PlayerSpawnPoint.transform.position;
            character.transform.rotation = m_PlayerSpawnPoint.transform.rotation;
        }
    }

    public void NextStep()
    {
        if (!m_IsLessonStepCompleted)
        {
            SkipToLessonStepEnd();
        } else
        {
            GoToLessonNextStep();
        }
    }

    public void SkipToLessonStepEnd()
    {
        OnLessonStepSkip?.Invoke(m_ClassroomLessonData.LessonSteps[m_CurrentDialogueIndex]);
        EndLessonStep();
    }

    public void EndLessonStep()
    {
        if (!m_IsLessonStepCompleted)
        {
            OnLessonStepEnd?.Invoke(m_ClassroomLessonData.LessonSteps[m_CurrentDialogueIndex]);
            m_IsLessonStepCompleted = true;
        }
    }

    public void OnLessonEnded(ClassroomLessonData classroomLessonData) { }

    public void GoToLessonNextStep()
    {
        ++m_CurrentDialogueIndex;

        if (m_CurrentDialogueIndex < m_ClassroomLessonData.LessonSteps.Count)
        {
            StartLessonStep(m_ClassroomLessonData.LessonSteps[m_CurrentDialogueIndex]);
        }

        if (m_CurrentDialogueIndex == m_ClassroomLessonData.LessonSteps.Count)
        {
            OnLessonEnd?.Invoke(m_ClassroomLessonData);
        }
    }

    public void StartLessonStep(LessonStep step)
    {
        m_IsLessonStepCompleted = false;

        OnLessonStepStart?.Invoke(step);
    }
}
