using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Experience_", menuName = "Experience")]
public class ExperienceData : ScriptableObject
{
    public ClassroomLessonData LessonData;

    [SerializeField]
    public SceneReference ExperienceScene;

    [SerializeField]
    public ExperienceData NextExperienceData;

    [Header("Allows you to setup parameters for your experience to load before it starts")]
    [SerializeField]
    public StringToBoolDictionary PreExperienceBoolSettings;

    [SerializeField]
    public StringToIntDictionary PreExperienceIntSettings;

    [SerializeField]
    public StringToStringDictionary PreExperienceStringSettings;

    public void UpdateSettings(PersistenceManager persistenceManager)
    {
        foreach (var kv in PreExperienceBoolSettings)
        {
            persistenceManager.SetBool(kv.Key, kv.Value);
        }

        foreach (var kv in PreExperienceIntSettings)
        {
            persistenceManager.SetInt(kv.Key, kv.Value);
        }

        foreach (var kv in PreExperienceStringSettings)
        {
            persistenceManager.SetString(kv.Key, kv.Value);
        }
    }
}
