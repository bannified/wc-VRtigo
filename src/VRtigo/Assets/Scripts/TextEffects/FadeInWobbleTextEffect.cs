using UnityEngine;
using TMPro;

public class FadeInWobbleTextEffect
{
	protected TMP_Text m_TextMesh;

	private float m_XWobbleFinal;
	private float m_YWobbleFinal;
	private float m_FadeDuration;
	private float m_WobbleRange;
	private float m_DurationSoFar;

	public FadeInWobbleTextEffect(TMP_Text textMesh, float xWobbleFinal, float yWobbleFinal, float fadeDuration, float wobbleRange)
	{
		SetText(textMesh);
		SetParameters(xWobbleFinal, yWobbleFinal, fadeDuration, wobbleRange);
	}

	public void SetText(TMP_Text textMesh)
	{
		textMesh.ForceMeshUpdate();

		m_TextMesh = textMesh;
	}

	public void SetParameters(float xWobbleFinal, float yWobbleFinal, float fadeDuration, float wobbleRange)
	{
		m_XWobbleFinal = xWobbleFinal;
		m_YWobbleFinal = yWobbleFinal;
		m_FadeDuration = fadeDuration;
		m_WobbleRange = wobbleRange;

		m_DurationSoFar = 0.0f;
	}

	public void Update()
	{
		if (m_DurationSoFar > m_FadeDuration) return;

		m_TextMesh.ForceMeshUpdate();
		Mesh mesh = m_TextMesh.mesh;
		Color[] colours = mesh.colors;
		Vector3[] vertices = mesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			float progress = m_DurationSoFar / m_FadeDuration;

			float amp = 1.0f + (m_WobbleRange * (1.0f - progress));
			float alpha = progress;
			Vector3 offset = WobbleWithAmplitude(Time.time + i, m_XWobbleFinal, m_YWobbleFinal, amp);

			vertices[i] += offset;
			colours[i].a = alpha;
		}

		// Update the actual vertices
		mesh.vertices = vertices;
		mesh.colors = colours;

		// Update the actual mesh
		m_TextMesh.UpdateGeometry(mesh, 0);

		m_DurationSoFar += Time.deltaTime;
	}

	/**
	 * Output a vector2 offset based on the given seed.
	 */
	private static Vector2 WobbleWithAmplitude(float seed, float xWobble, float yWobble, float amplify)
	{
		return new Vector2(Mathf.Sin(seed * xWobble * amplify), Mathf.Cos(seed * yWobble * amplify));
	}
}
