using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassroomLesson_", menuName = "Classroom Lesson")]
public class ClassroomLessonData : ScriptableObject
{
    public string ClassroomTitle;

    [SerializeField]
    public List<LessonStep> LessonSteps;
}
