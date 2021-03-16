using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = target as GameManager;

        if (target == null)
        {
            return;
        }

        if (!Application.isPlaying)
        {
            return;
        }

        if (GUILayout.Button("Start Attached Experience"))
        {
            gameManager.StartExperience();
        }

        if (GUILayout.Button("Start Attached Experience's Next Experience"))
        {
            gameManager.StartNextExperience();
        }

        if (GUILayout.Button("Start Lesson for Attached Experience"))
        {
            ClassroomManager.Instance.StartLesson(GameManager.Instance.GetCurrentExperience().LessonData);
        }
    }
}
