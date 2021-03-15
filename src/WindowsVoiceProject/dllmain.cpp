#include "pch.h"

#include "WindowsVoice.h"

#include <sapi.h>
#pragma warning(disable:4996)
#include <sphelper.h>
#pragma warning(default:4996) 
//#include <iostream>

namespace WindowsVoice {

  void speechThreadFunc()
  {
      SetStatus(VoiceStatus::Initializing);
      pVoice = NULL;

    if (FAILED(::CoInitializeEx(NULL, COINITBASE_MULTITHREADED)))
    {
        SetStatusMessage(L"Failed to initialize COM for Voice.");
      return;
    }

    HRESULT hr = CoCreateInstance(CLSID_SpVoice, NULL, CLSCTX_ALL, IID_ISpVoice, (void **)&pVoice);
    if (!SUCCEEDED(hr))
    {
      LPSTR pText = 0;

      ::FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
        NULL, hr, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), pText, 0, NULL);
      LocalFree(pText);
      SetStatusMessage(L"Failed to create Voice instance.");
      return;
    }

    CComPtr<ISpAudio> audio;
    CSpStreamFormat format;
    format.AssignFormat(SPSF_Default);
    SpCreateDefaultObjectFromCategoryId(SPCAT_AUDIOOUT, &audio);
    voiceAudio = audio;
    audio->SetFormat(format.FormatId(), format.WaveFormatExPtr());
    pVoice->SetOutput(audio, FALSE);

    SetStatusMessage(L"Speech ready.");
/*
    //std::cout << "Speech ready.\n";
    wchar_t* priorText = nullptr;
    while (!shouldTerminate)
    {
      wchar_t* wText = NULL;
      if (!theSpeechQueue.empty())
      {
        theMutex.lock();
        wText = theSpeechQueue.front();
        theSpeechQueue.pop_front();
        theMutex.unlock();
      }
      if (wText)
      {
        if (priorText == nullptr || lstrcmpW(wText, priorText) != 0)
        {
          pVoice->Speak(wText, SPF_IS_XML, NULL);
          Sleep(250);
          delete[] priorText;
          priorText = wText;
        }
        else
          delete[] wText;
      }
      else
      {
        delete[] priorText;
        priorText = nullptr;
        Sleep(50);
      }
    }
    pVoice->Release();
*/
    SPVOICESTATUS voiceStatus;
    wchar_t* priorText = nullptr;
    while (!shouldTerminate)
    {
      pVoice->GetStatus(&voiceStatus, NULL);
      if (voiceStatus.dwRunningState == SPRS_IS_SPEAKING)
      {
        if (priorText == nullptr)
          SetStatusMessage(L"Error: SPRS_IS_SPEAKING but text is NULL");
        else
        {
          SetStatus(VoiceStatus::Speaking);
          std::wstring msg = L"Speaking: ";
          msg.append(priorText);
          SetStatusMessage(msg);
          theMutex.lock();
          if (!theSpeechQueue.empty())
          {
            if (lstrcmpW(theSpeechQueue.front(), priorText) == 0)
            {
              delete[] theSpeechQueue.front();
              theSpeechQueue.pop_front();
            }
          }
          theMutex.unlock();
        }
      }
      else
      {
        SetStatus(VoiceStatus::Waiting);
        SetStatusMessage(L"Waiting.");
        if (priorText != NULL)
        {
          delete[] priorText;
          priorText = NULL;
        }
        theMutex.lock();
        if (!theSpeechQueue.empty())
        {
          priorText = theSpeechQueue.front();
          theSpeechQueue.pop_front();
          audioMutex.lock();
          voiceAudio->SetState(SPAS_RUN, 0);
          pVoice->Speak(priorText, SPF_IS_XML | SPF_ASYNC | SPF_PURGEBEFORESPEAK, NULL);
          audioMutex.unlock();
        }
        theMutex.unlock();
      }
      Sleep(20);
    }
    SetStatus(VoiceStatus::Terminating);
    pVoice->Pause();
    pVoice->Release();

    SetStatusMessage(L"Speech thread terminated.");
  }

  void setStatusUpdateCallback(WV_CALLBACK callback)
  {
      statusUpdateCallback = callback;
  }

  void addToSpeechQueue(const char* text)
  {
    if (text)
    {
      int len = strlen(text) + 1;
      wchar_t *wText = new wchar_t[len];

      memset(wText, 0, len);
      ::MultiByteToWideChar(CP_UTF8, NULL, text, -1, wText, len);

      theMutex.lock();
      theSpeechQueue.push_back(wText);
      theMutex.unlock();
    }
  }

  bool getIsInitialized()
  {
      return theSpeechThread != nullptr;
  }

  void clearSpeechQueue()
  {
    theMutex.lock();
    theSpeechQueue.clear();
    theMutex.unlock();
  }

  void initSpeech()
  {
    shouldTerminate = false;
    if (theSpeechThread != nullptr)
    {
        SetStatusMessage(L"Windows Voice thread already started.");
      return;
    }
    SetStatusMessage(L"Starting Windows Voice.");
    theSpeechThread = new std::thread(WindowsVoice::speechThreadFunc);
  }

  void stopSpeech()
  {
      //pVoice->Pause();
      audioMutex.lock();
      voiceAudio->SetState(SPAS_STOP, 0);
      audioMutex.unlock();
  }

  void destroySpeech()
  {
    if (theSpeechThread == nullptr)
    {
        SetStatusMessage(L"Speach thread already destroyed or not started.");
      return;
    }
    SetStatusMessage(L"Destroying speech.");
    shouldTerminate = true;
    theSpeechThread->join();
    theSpeechQueue.clear();
    delete theSpeechThread;
    theSpeechThread = nullptr;
    statusUpdateCallback = nullptr;
    CoUninitialize();
    SetStatusMessage(L"Speech destroyed.");
  }

  void statusMessage(char* msg, int msgLen)
  {
    size_t count;
    wcstombs_s(&count, msg, msgLen, theStatusMessage.c_str(), msgLen);
  }

  void SetStatus(VoiceStatus status)
  {
      VoiceStatus previous = currentStatus;
      currentStatus = status;
      if (previous != currentStatus)
      {
          if (statusUpdateCallback != nullptr) 
          {
              statusUpdateCallback(status);
          }
      }
  }

  void SetStatusMessage(const std::wstring& message)
  {
      theStatusMessage = message;
  }
}

BOOL APIENTRY DllMain(HMODULE, DWORD ul_reason_for_call, LPVOID)
{
  switch (ul_reason_for_call)
  {
  case DLL_PROCESS_ATTACH:
  case DLL_THREAD_ATTACH:
  case DLL_THREAD_DETACH:
  case DLL_PROCESS_DETACH:
    break;
  }
  
  return TRUE;
}