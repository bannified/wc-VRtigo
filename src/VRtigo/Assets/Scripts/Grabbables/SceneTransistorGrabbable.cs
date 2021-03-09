using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable : MonoBehaviour, IGrabbable
{
    [SerializeField]
    protected string m_SceneName;

    public void Grabbed() { }

    public void Dropped() { }

    public string GetSceneName()
    {
        return m_SceneName;
    }
}
