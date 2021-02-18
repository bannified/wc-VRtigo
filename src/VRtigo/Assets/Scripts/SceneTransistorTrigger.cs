using UnityEngine;

public class SceneTransistorTrigger : MonoBehaviour
{
	public SceneTransistor sceneTransistor;

	public string sceneName;        // Name of scene file to be loaded
	public string sceneTitle;       // Name of scene to be shown to the player during transition
	public Vector3 titleOffset;     // Offset for title relative to this.transform.position
	public float titleDuration;     // Duration for how long the title should be displayed, including the fadeIn time

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// TODO: VR Interaction
			Interact();
		}
	}

	public void Interact()
	{
		// TODO: Block player input to prevent multiple scene transition

		// Fade to the next scene
		sceneTransistor.FadeToScene(sceneName, sceneTitle, transform.position + titleOffset, titleDuration);
	}
}
