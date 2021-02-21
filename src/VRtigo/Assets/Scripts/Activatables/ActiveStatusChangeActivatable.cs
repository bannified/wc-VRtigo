using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStatusChangeActivatable : MonoBehaviour, IActivatable
{
	[SerializeField]
	private GameObject m_TargetGameObject;

	[SerializeField]
	private bool m_SetActive = true;

	public void Activate()
	{
		if (m_TargetGameObject == null)
		{
			return;
		}

		m_TargetGameObject.SetActive(m_SetActive);
	}

	public void OnHoverIn() { }

	public void OnHoverOut() { }

}
