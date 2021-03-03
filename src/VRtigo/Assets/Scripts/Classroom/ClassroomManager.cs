using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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

    /// <summary>
    /// Classroom Events
    /// </summary>
    public System.Action<ClassroomLessonData> OnLessonStart;
    public System.Action<LessonStep> OnLessonStepStart;
    public System.Action<LessonStep> OnLessonStepSkip;
    public System.Action<LessonStep> OnLessonStepEnd;

    public void StartLesson(ClassroomLessonData classroomData)
    {
        if (classroomData == null)
        {
            return;
        }

        m_ClassroomLessonData = classroomData;
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
        if (SteamVR.active)
        {
            VRT_Helpers.ResetHMDPosition();
        }

        if (m_AutoplayClassroom && m_ClassroomLessonData != null)
        {
            StartLesson();
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
        OnLessonStepEnd?.Invoke(m_ClassroomLessonData.LessonSteps[m_CurrentDialogueIndex]);
        m_IsLessonStepCompleted = true;
    }

    public void GoToLessonNextStep()
    {
        ++m_CurrentDialogueIndex;

        if (m_CurrentDialogueIndex < m_ClassroomLessonData.LessonSteps.Count)
        {
            StartLessonStep(m_ClassroomLessonData.LessonSteps[m_CurrentDialogueIndex]);
        }
    }

    public void StartLessonStep(LessonStep step)
    {
        m_IsLessonStepCompleted = false;

        OnLessonStepStart?.Invoke(step);
    }
}
