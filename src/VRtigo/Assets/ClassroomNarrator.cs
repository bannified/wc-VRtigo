using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomNarrator : MonoBehaviour
{

    public void NarrateString(string message)
    {
        // [TODO]: Replace with playing actual audio files generated from TTS
        TextToSpeechManager.stopSpeech();
        TextToSpeechManager.addToSpeechQueue(message);
    }

    private void OnDisable()
    {
        TextToSpeechManager.stopSpeech();
    }
}
