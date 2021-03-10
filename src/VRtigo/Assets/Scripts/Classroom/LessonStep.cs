using UnityEngine;

/// <summary>
/// A class that's only meant to store data (NO LOGIC) regarding a Lesson Step, to be used by ClassroomManager to run the lesson.
/// We won't use polymorphism due to Unity not being able to serialize a list of objects with different subclasses.
/// Therefore, we'll include all kinds of data, used or not, in this class.
/// So if you want to add a new property to a Lesson Step, add it here.
/// This class will get big and include unnecessary properties, but this is the best way without having to worry
/// about writing custom inspectors and rely on the default inspector Unity has.
/// </summary>
[System.Serializable]
public class LessonStep
{
    [TextArea(3, 5)]
    public string m_DialogueString;

    public LessonStep()
    {
        m_DialogueString = "Lorem Ipsum blah";
    }
}