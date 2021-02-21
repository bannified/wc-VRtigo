using UnityEngine;

public class SceneTransistorTrigger : MonoBehaviour
{
	public SceneTransistor sceneTransistor;

	public string sceneName;        // Name of scene file to be loaded

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
		sceneTransistor.FadeToScene(sceneName);
	}
}
