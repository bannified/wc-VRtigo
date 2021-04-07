using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public enum VRManufacturer
{
    HTC,
    Oculus,
    Valve,

    NONE
}

public class PlatformDependentVisibilityController : MonoBehaviour
{
    [SerializeField]
    private VRManufacturer m_ManufacturerType = VRManufacturer.NONE;

    private static float m_CheckingTime = 2.0f;

    private Coroutine m_CheckingCoroutine;

    [SerializeField]
    private string m_PersistenceKeyToCheck = "";

    [SerializeField]
    private bool m_TargetBool = false;

    private void Start()
    {
        if (m_PersistenceKeyToCheck != "")
        {
            bool boolValue = false;
            PersistenceManager.Instance.TryGetBool(m_PersistenceKeyToCheck, ref boolValue);
            if (boolValue == m_TargetBool)
            {
                m_CheckingCoroutine = StartCoroutine(CheckForDeviceCoroutine());
            } else
            {
                this.gameObject.SetActive(false);
            }
        } else
        {
            m_CheckingCoroutine = StartCoroutine(CheckForDeviceCoroutine());
        }
    }

    private IEnumerator CheckForDeviceCoroutine()
    {
        string manufacturerString = "";
        switch (m_ManufacturerType)
        {
            case VRManufacturer.HTC:
                manufacturerString = "htc";
                break;
            case VRManufacturer.Oculus:
                manufacturerString = "oculus";
                break;
            case VRManufacturer.Valve:
                break;
            case VRManufacturer.NONE:
            default:
                manufacturerString = "";
                break;
        }


        float runningTime = 0.0f;
        while (runningTime < m_CheckingTime)
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            UnityEngine.XR.InputDevices.GetDevices(inputDevices);

            foreach (var device in inputDevices)
            {
                if (device.manufacturer.ToLower() != manufacturerString)
                {
                    this.gameObject.SetActive(false);
                    StopCoroutine(m_CheckingCoroutine);
                    break;
                }
            }
            runningTime += Time.deltaTime;
            yield return null;
        }

        List<InputDevice> inputDevices2 = new List<InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices2);
        if (inputDevices2.Count == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
