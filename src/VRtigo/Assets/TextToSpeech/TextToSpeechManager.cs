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

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class TextToSpeechManager
{
    [DllImport("WindowsVoice")]
    public static extern bool getIsInitialized();
    [DllImport("WindowsVoice")]
    public static extern void initSpeech();
    [DllImport("WindowsVoice")]
    public static extern void destroySpeech();
    [DllImport("WindowsVoice")]
    public static extern void addToSpeechQueue(string s);
    [DllImport("WindowsVoice")]
    public static extern void clearSpeechQueue();
    [DllImport("WindowsVoice")]
    public static extern void stopSpeech();
    [DllImport("WindowsVoice")]
    public static extern void statusMessage(StringBuilder str, int length);

    [DllImport("WindowsVoice")]
    public static extern void setStatusUpdateCallback(statusUpdateCallback callback);

    //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void statusUpdateCallback(VoiceStatus status);

    private static statusUpdateCallback m_StatusUpdateCallback;

    static TextToSpeechManager()
    {
#if UNITY_EDITOR
        AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;

        EditorApplication.quitting -= OnEditorApplicationQuitting;
        EditorApplication.quitting += OnEditorApplicationQuitting;
#else
        Application.quitting -= OnApplicationQuitting;
        Application.quitting += OnApplicationQuitting;
#endif

        m_StatusUpdateCallback = new statusUpdateCallback(OnStatusUpdate);
        setStatusUpdateCallback(m_StatusUpdateCallback);

        if (!getIsInitialized())
        {
            initSpeech();
        }
    }

#if !UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeLoad()
    {
        // This forces the static constructor to be called at runtime
    }
#endif

    private static void OnBeforeAssemblyReload()
    {
        if (getIsInitialized())
        {
            destroySpeech();
        }
    }

    private static void OnStatusUpdate(VoiceStatus status)
    {
        Debug.Log(string.Format("TTS Manage status changed: {0}", status.ToString()));
    }

    public static string GetStatusMessage(int additionalLength = 0)
    {
        StringBuilder sb = new StringBuilder(40 + additionalLength);
        statusMessage(sb, 40 + additionalLength);
        return sb.ToString();
    }
#if UNITY_EDITOR
    private static void OnEditorApplicationQuitting()
    {
        Debug.Log("TextToSpeechManager.OnEditorApplicationQuitting");
        destroySpeech();
    }
#else
    private static void OnApplicationQuitting()
    {
        Debug.Log("TextToSpeechManager.OnApplicationQuitting");
        destroySpeech();
    }
#endif
}
