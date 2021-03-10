using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Text;

public enum VoiceStatus : sbyte
{
    INVALID, 
    Initializing,
    Waiting,
    Speaking,
    Terminating
}

[InitializeOnLoad]
public class TextToSpeechManager
{
    [DllImport("WindowsVoice")]
    public static extern void initSpeech();
    [DllImport("WindowsVoice")]
    public static extern void destroySpeech();
    [DllImport("WindowsVoice")]
    public static extern void addToSpeechQueue(string s);
    [DllImport("WindowsVoice")]
    public static extern void clearSpeechQueue();
    [DllImport("WindowsVoice")]
    public static extern void statusMessage(StringBuilder str, int length);

    [DllImport("WindowsVoice")]
    public static extern void setStatusUpdateCallback(statusUpdateCallback callback);

    //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void statusUpdateCallback(VoiceStatus status);

    private static bool bIsInitialized = false;

    static TextToSpeechManager()
    {
        if (bIsInitialized)
        {
            destroySpeech();
        }

        setStatusUpdateCallback(new statusUpdateCallback(OnStatusUpdate));
        bIsInitialized = true;
        initSpeech();
        EditorApplication.quitting += OnEditorApplicationQuitting;
    }

    private static void OnStatusUpdate(VoiceStatus status)
    {
        Debug.Log(string.Format("TTS Manage status changed: {0}", status.ToString()));

    }

    private static void OnEditorApplicationQuitting()
    {
        Debug.Log("TextToSpeechManager.OnEditorApplicationQuitting");
        destroySpeech();
    }
}
