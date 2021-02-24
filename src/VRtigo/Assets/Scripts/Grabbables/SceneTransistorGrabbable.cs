using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransistorGrabbable : MonoBehaviour, IGrabbable
{
	[SerializeField]
	protected string m_SceneName;

	[SerializeField]
	protected SceneTransistor m_SceneTransistor;

	void Start()
	{
		m_SceneTransistor = GameObject.FindGameObjectWithTag("SceneTransistor").GetComponent<SceneTransistor>();
		Debug.Assert(m_SceneTransistor != null, "SceneTransistorActivatable requires SceneTransistor");
	}


	public void Grabbed()
	{
		m_SceneTransistor.FadeToScene(m_SceneName);
	}

	public void Dropped() { }
}
