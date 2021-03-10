#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport)
#else
#define DLL_API __declspec(dllimport)
#endif

#include <mutex>
#include <list>
#include <thread>

enum class VoiceStatus : __int8
{
    INVALID, 

    Initializing,
    Waiting,
    Speaking,
    Terminating,
};

struct ISpVoice;
struct ISpAudio;

namespace WindowsVoice {

  typedef void (__stdcall* WV_CALLBACK)(VoiceStatus);

  extern "C" {
    DLL_API bool __cdecl getIsInitialized();
    DLL_API void __cdecl initSpeech();
    DLL_API void __cdecl addToSpeechQueue(const char* text);
    DLL_API void __cdecl clearSpeechQueue();
    DLL_API void __cdecl stopSpeech();
    DLL_API void __cdecl destroySpeech();
    DLL_API void __cdecl statusMessage(char* msg,  int msgLen);
    DLL_API void __cdecl setStatusUpdateCallback(WV_CALLBACK callback);
  }

  WV_CALLBACK statusUpdateCallback;

  void SetStatus(VoiceStatus status);
  void SetStatusMessage(const std::wstring& message);

  VoiceStatus currentStatus = VoiceStatus::INVALID;
  std::mutex theMutex;
  std::mutex audioMutex;
  std::list<wchar_t*> theSpeechQueue;
  std::thread* theSpeechThread = nullptr;
  bool shouldTerminate = false;
  bool isSkipping = false;
  ISpVoice* pVoice;
  ISpAudio* voiceAudio;

  std::wstring theStatusMessage;
}