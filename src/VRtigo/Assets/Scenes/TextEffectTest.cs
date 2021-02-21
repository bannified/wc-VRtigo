using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffectTest : MonoBehaviour
{
	public TMP_Text textMesh;
	public float maxScatterOffset = 11.1f;
	public float duration = 1f;
	public bool isScatter = false;

	public FadeOutScatterTextEffect scatter;
	public FadeInWobbleTextEffect fadeIn;

	// Start is called before the first frame update
	void Start()
	{
		textMesh = GetComponent<TMP_Text>();
		//scatter = new FadeOutScatterTextEffect(textMesh, maxScatterOffset, duration);
		fadeIn = new FadeInWobbleTextEffect(textMesh, 0.05f, 0.01f, 5.0f, 3.0f);
	}

	// Update is called once per frame
	void Update()
	{
		if (isScatter)
			fadeIn.Update();
		//scatter.Update();
	}
}
