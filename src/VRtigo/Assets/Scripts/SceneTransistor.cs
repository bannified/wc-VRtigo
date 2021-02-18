using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransistor : MonoBehaviour
{
	private Animator sceneTransistorAnimator;
	private TMP_Text sceneTitleTMP;

	private string sceneName = "SampleScene";

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		this.sceneTransistorAnimator = this.GetComponent<Animator>();
		this.sceneTitleTMP = this.GetComponentInChildren<TMP_Text>();

		// Reset transform
		this.transform.position = Vector3.zero;
		this.transform.rotation = Quaternion.identity;

		// Set it to world space
		Canvas fadeCanvas = this.GetComponent<Canvas>();
		fadeCanvas.renderMode = RenderMode.WorldSpace;
		fadeCanvas.worldCamera = Camera.main;

		Vector3 cameraForward = Camera.main.transform.forward;
		cameraForward.y = 0.0f;

		// Make sure black screen is always in front of camera and rotated correctly
		Image blackScreen = this.GetComponentInChildren<Image>();
		blackScreen.transform.position = Camera.main.transform.position + cameraForward;
		blackScreen.transform.rotation = Quaternion.LookRotation(cameraForward);

		// Set camera as parent, so it moves as camera moves
		this.transform.SetParent(Camera.main.transform);
	}

	/**
	 * Fade the screen to black and load the scene with the given name.
	 * sceneName:		name of scene file to be loaded
	 * sceneTitle:		name of scene to be shown to the player during transition
	 * sceneTitlePos:	world position of where should the title be shown
	 * sceneTitleDur:	duration in which the scene of title to be shown (in seconds), including fadeIn time
	 */
	public void FadeToScene(string sceneName, string sceneTitle, Vector3 sceneTitlePos, float sceneTitleDur)
	{
		this.sceneName = sceneName;

		// Set text to be shown
		this.sceneTitleTMP.transform.position = sceneTitlePos - this.transform.position;
		this.sceneTitleTMP.SetText(sceneTitle);

		// Start animation
		sceneTransistorAnimator.SetTrigger("FadeInText");

		// Fade out text in the future
		StartCoroutine("TriggerFadeOutText", sceneTitleDur);
	}

	/**
	 * This is called from the FadeOut animation.
	 */
	public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}

	/**
	* This is called from the FadeOutText animation.
	*/
	public void ResetText()
	{
		this.sceneTitleTMP.SetText("");
	}

	private IEnumerator TriggerFadeOutText(float delayTime)
	{
		// Wait for the specified delay time before continuing.
		yield return new WaitForSeconds(delayTime);

		// Do the action after the delay time has finished.
		sceneTransistorAnimator.SetTrigger("FadeOutText");
		yield return null;
	}
}
