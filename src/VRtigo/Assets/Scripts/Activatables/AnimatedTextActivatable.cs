using System.Collections;
using UnityEngine;
using TMPro;

public class AnimatedTextActivatable : MonoBehaviour, IActivatable
{
	[Header("Text Settings")]
	[SerializeField]
	protected TMP_Text m_Text;
	[SerializeField]
	protected string m_TextToShow;

	[Header("Animation Parameters")]
	[SerializeField]
	protected Animator m_TextAnimator;
	[SerializeField]
	protected float m_ShowDuration = 5.0f; // Including fade in time, in seconds
	[SerializeField]
	protected string m_FadeInTriggerName = "FadeInText";
	[SerializeField]
	protected string m_FadeOutTriggerName = "FadeOutText";

	void Start()
	{
		Debug.Assert(m_Text != null, "TMP_Text is not attached");
		Debug.Assert(m_TextAnimator != null, "Animator is not attached");

		m_Text.SetText("");
	}

	public void Activate()
	{
		// Reset
		m_Text.SetText("");

		m_TextAnimator.SetTrigger(m_FadeInTriggerName);
		m_Text.SetText(m_TextToShow);

		StartCoroutine("TriggerFadeOutText", m_ShowDuration);
	}

	public void OnHoverIn() { }

	public void OnHoverOut() { }

	private IEnumerator TriggerFadeOutText(float delayTime)
	{
		// Wait for the specified delay time before continuing.
		yield return new WaitForSeconds(delayTime);

		// Do the action after the delay time has finished.
		m_TextAnimator.SetTrigger(m_FadeOutTriggerName);
		yield return null;
	}
}
