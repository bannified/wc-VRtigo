using UnityEngine;
using TMPro;

public class WobbleTextEffect
{
	protected float m_Progress;
	protected TMP_Text m_TextMesh;

	private float m_XWobble;
	private float m_YWobble;

	public WobbleTextEffect(TMP_Text textMesh, float xWobble, float yWobble)
	{
		SetText(textMesh);
		SetParameters(xWobble, yWobble);
	}

	public void SetText(TMP_Text textMesh)
	{
		textMesh.ForceMeshUpdate();

		m_TextMesh = textMesh;
	}

	public void SetParameters(float xWobble, float yWobble)
	{
		m_XWobble = xWobble;
		m_YWobble = yWobble;

		m_Progress = 1.0f;
	}

	public void Update()
	{
		m_TextMesh.ForceMeshUpdate();
		Mesh mesh = m_TextMesh.mesh;
		Vector3[] vertices = mesh.vertices;

		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 offset = Wobble(Time.time + i, m_XWobble, m_YWobble);
			vertices[i] += offset;
		}

		// Update the actual vertices
		mesh.vertices = vertices;

		// Update the actual mesh
		m_TextMesh.UpdateGeometry(mesh, 0);
	}

	public float GetProgress()
	{
		return m_Progress;
	}

	/**
	 * Output a vector2 offset based on the given seed.
	 */
	private static Vector2 Wobble(float seed, float xWobble, float yWobble)
	{
		return new Vector2(Mathf.Sin(seed * xWobble), Mathf.Cos(seed * yWobble));
	}
}
