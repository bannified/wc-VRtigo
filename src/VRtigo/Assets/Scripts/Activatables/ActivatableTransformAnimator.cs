using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableTransformAnimator : MonoBehaviour, IActivatable
{
	private Coroutine m_Coroutine;

	[SerializeField]
	private float m_DelayInSeconds;

	[SerializeField]
	private float m_AnimationDuration;

	[SerializeField]
	private Transform m_TargetTransform;

	[SerializeField]
	Vector3 m_ResultPosition;

	[SerializeField]
	Vector3 m_ResultScale = new Vector3(1.0f, 1.0f, 1.0f);

	[SerializeField]
	Vector3 m_ResultRotation;

	public void Activate()
	{
		m_Coroutine = StartCoroutine(TransformAnimationCoroutine());
	}

	public void OnHoverIn() { }

	public void OnHoverOut() { }


	private IEnumerator TransformAnimationCoroutine()
	{
		WaitForSeconds wfs = new WaitForSeconds(m_DelayInSeconds);
		yield return wfs;

		Vector3 startingPosition = m_TargetTransform.localPosition;
		Vector3 startingScale = m_TargetTransform.localScale;
		Vector3 startingRotation = m_TargetTransform.localEulerAngles;

		for (float t = 0.0f; t <= m_AnimationDuration; t += Time.deltaTime)
		{
			m_TargetTransform.localPosition = Vector3.Lerp(startingPosition, m_ResultPosition, t / m_AnimationDuration);
			m_TargetTransform.localScale = Vector3.Lerp(startingScale, m_ResultScale, t / m_AnimationDuration);
			m_TargetTransform.localEulerAngles = Vector3.Lerp(startingRotation, m_ResultRotation, t / m_AnimationDuration);

			yield return null;
		}

		m_TargetTransform.localPosition = m_ResultPosition;
		m_TargetTransform.localScale = m_ResultScale;
		m_TargetTransform.localEulerAngles = m_ResultRotation;

		yield return null;
	}
}
