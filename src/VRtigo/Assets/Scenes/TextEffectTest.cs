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

	// Start is called before the first frame update
	void Start()
	{
		textMesh = GetComponent<TMP_Text>();
		scatter = new FadeOutScatterTextEffect(textMesh, maxScatterOffset, duration);
	}

	// Update is called once per frame
	void Update()
	{
		if (isScatter)
			scatter.Update();
	}
}
