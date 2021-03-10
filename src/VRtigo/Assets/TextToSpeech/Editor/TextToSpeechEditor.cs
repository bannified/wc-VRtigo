using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextToSpeechEditor : EditorWindow
{
    private string m_InputText;

    [MenuItem("VRtigo/Text-To-Speech")]
    public static void ShowWindow()
    {
        // Show existing window instance, If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(TextToSpeechEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Text to Speech Editor", EditorStyles.boldLabel);

        m_InputText = GUILayout.TextArea(m_InputText, new GUILayoutOption[] { GUILayout.ExpandHeight(true) });

        if (GUILayout.Button("Play Text as Speech"))
        {
            TextToSpeechManager.addToSpeechQueue(m_InputText);
        }

        if (GUILayout.Button("Clear Speech queue"))
        {
            TextToSpeechManager.clearSpeechQueue();
        }
    }
}
