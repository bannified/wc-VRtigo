using UnityEngine;
using TMPro;

public interface ITextEffect
{
	/**
	 * Update() must be called within MonoBehaviour's Update() to apply the effect.
	 */
	public void Update();

	/**
	 * Returns a float [0, 1] to indicate the progress of effect,
	 * 1.0f indicates that the effect has finished.
	 */
	public float GetProgress();
}
