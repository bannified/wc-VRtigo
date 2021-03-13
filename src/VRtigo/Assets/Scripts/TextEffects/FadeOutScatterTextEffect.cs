using UnityEngine;
using TMPro;

public class FadeOutScatterTextEffect
{
    protected TMP_Text m_TextMesh;
    protected float m_Progress;

    private float m_MaxScatterRange;    // Maximum range traveled by any character during scatter effect
    private float m_ScatterDuration;    // Total duration of scatter effect
    private float m_ScatterStep;        // How much distance traveled by any character in each frame
    private float m_DurationSoFar;      // Duration of effect so far

    private Vector3[] m_PrevVerts;      // Position of vertices from the previous frame
    private Vector3 m_TextOrigin;       // Characters will scatter away from the text origin

    public FadeOutScatterTextEffect(TMP_Text textMesh, float maxScatterRange, float duration)
    {
        SetText(textMesh);
        SetParameters(maxScatterRange, duration);
        Reset();
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
        m_ScatterStep = m_MaxScatterRange * Time.deltaTime / m_ScatterDuration;
    }

    public void Reset()
    {
        m_DurationSoFar = 0.0f;
        m_Progress = 0.0f;
    }

    public void Update()
    {
        if (m_DurationSoFar > m_ScatterDuration) return;

        m_TextMesh.ForceMeshUpdate();

        Mesh mesh = m_TextMesh.mesh;
        Color[] colours = mesh.colors;

        // Update progress by tracking duration
        m_Progress = m_DurationSoFar / m_ScatterDuration;

        for (int i = 0; i < m_TextMesh.textInfo.characterCount; i++)
        {
            // Instead by each vertices, we do by each character
            TMP_CharacterInfo c = m_TextMesh.textInfo.characterInfo[i];
            int index = c.vertexIndex;

            Vector3 offset = GetScatterOffsetForChar(m_PrevVerts, index, m_TextOrigin, m_ScatterStep);
            // Alpha decreases as progress increases
            float alpha = 1.0f - (m_Progress + Time.deltaTime / m_ScatterDuration);

            // Each character has 4 vertices, update them
            for (int j = index; j < index + 4; j++)
            {
                m_PrevVerts[j] += offset;
                colours[j].a = alpha;
            }
        }

        // Update the actual vertices
        mesh.vertices = m_PrevVerts;
        mesh.colors = colours;

        // Update the actual mesh
        m_TextMesh.UpdateGeometry(mesh, 0);

        // Update duration
        m_DurationSoFar += Time.deltaTime;
    }

    public float GetProgress()
    {
        return m_Progress;
    }

    public Vector3[] GetVerticesPosition()
    {
        return m_PrevVerts;
    }
    public void SetVerticesPosition(Vector3[] newPos)
    {
        m_PrevVerts = newPos;
    }

    /**
     * Produces slightly perturbed direction away from the origin, scaled by the offset argument
     */
    private Vector2 GetScatterOffsetForChar(Vector3[] vertices, int index, Vector3 origin, float offset)
    {
        Vector3 to = vertices[index + Random.Range(0, 3)];
        return (to - origin).normalized * offset;
    }
}
