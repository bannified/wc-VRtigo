using UnityEngine;
using TMPro;

public class FadeOutScatterTextEffect
{
	protected TMP_Text m_TextMesh;

	private Vector3[] m_PrevVerts;
	private Vector3 m_TextOrigin;

	private float m_MaxScatterRange;
	private float m_ScatterDuration;
	private float m_ScatterStep;
	private float m_DurationSoFar;

	public FadeOutScatterTextEffect(TMP_Text textMesh, float maxScatterRange, float duration)
	{
		SetText(textMesh);
		SetParameters(maxScatterRange, duration);
	}

	public void SetText(TMP_Text textMesh)
	{
		textMesh.ForceMeshUpdate();

		m_TextMesh = textMesh;
		m_PrevVerts = textMesh.mesh.vertices;
		m_TextOrigin = textMesh.transform.root.position;
	}

	public void SetParameters(float maxScatterRange, float duration)
	{
		m_MaxScatterRange = maxScatterRange;
		m_ScatterDuration = duration;

		m_DurationSoFar = 0.0f;
		m_ScatterStep = m_MaxScatterRange * Time.deltaTime / m_ScatterDuration;
	}

	public void Update()
	{
		if (m_DurationSoFar > m_ScatterDuration) return;

		m_TextMesh.ForceMeshUpdate();

		Mesh mesh = m_TextMesh.mesh;
		Color[] colours = mesh.colors;

		for (int i = 0; i < m_TextMesh.textInfo.characterCount; i++)
		{
			TMP_CharacterInfo c = m_TextMesh.textInfo.characterInfo[i];
			int index = c.vertexIndex;

			Vector3 offset = GetScatterOffsetForChar(m_PrevVerts, index, m_TextOrigin, m_ScatterStep);
			float alpha = 1.0f - (m_DurationSoFar / m_ScatterDuration);

			for (int j = index; j < index + 4; j++)
			{
				m_PrevVerts[j] += offset;
				colours[j].a = alpha;
			}

			m_DurationSoFar += Time.deltaTime;
		}

		mesh.vertices = m_PrevVerts;
		mesh.colors = colours;

		m_TextMesh.UpdateGeometry(mesh, 0);
	}

	private Vector2 GetScatterOffsetForChar(Vector3[] vertices, int index, Vector3 origin, float offset)
	{
		Vector3 to = vertices[index + Random.Range(0, 3)];
		return (to - origin).normalized * offset;
	}
}
