using UnityEngine;
using TMPro;

public class FadeInWobbleTextEffect
{
    protected TMP_Text m_TextMesh;
    protected float m_Progress;

    private float m_XWobbleFinal;       // Extent of wobble in x direction after fade in finishes
    private float m_YWobbleFinal;       // Extent of wobble in y direction after fade in finishes
    private float m_FadeDuration;       // Total duration of fade in effect
    private float m_WobbleRange;        // During fade in, the extent of wobble will interpolate from (wobbleFinal + range) to wobbleFinal
    private float m_DurationSoFar;      // Duration of effect so far

    private Vector3[] m_PrevVerts;      // Position of vertices from the previous frame

    public FadeInWobbleTextEffect(TMP_Text textMesh, float xWobbleFinal, float yWobbleFinal, float fadeDuration, float wobbleRange)
    {
        SetText(textMesh);
        SetParameters(xWobbleFinal, yWobbleFinal, fadeDuration, wobbleRange);
        Reset();
    }

    public void SetText(TMP_Text textMesh)
    {
        textMesh.ForceMeshUpdate();

        m_TextMesh = textMesh;
        m_PrevVerts = textMesh.mesh.vertices;
    }

    public void SetParameters(float xWobbleFinal, float yWobbleFinal, float fadeDuration, float wobbleRange)
    {
        m_XWobbleFinal = xWobbleFinal;
        m_YWobbleFinal = yWobbleFinal;
        m_FadeDuration = fadeDuration;
        m_WobbleRange = wobbleRange;
    }

    public void Reset()
    {
        m_DurationSoFar = 0.0f;
        m_Progress = 0.0f;
    }

    public void Update()
    {
        m_TextMesh.ForceMeshUpdate();
        Mesh mesh = m_TextMesh.mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colours = mesh.colors;

        float amp = 1.0f;
        float alpha = 1.0f;

        // We are still in fade in
        if (m_DurationSoFar < m_FadeDuration)
        {
            // Update progress by tracking duration
            m_Progress = m_DurationSoFar / m_FadeDuration;

            // Extent of wobble decreases as progress increases
            amp = 1.0f + m_WobbleRange * (1.0f - m_Progress);

            // Since progress is [0, 1], we can use it for alpha too
            alpha = m_Progress;

            // Update duration
            m_DurationSoFar += Time.deltaTime;
        }

        Vector3 offset = Vector3.zero;
        for (int i = 0; i < vertices.Length; i++)
        {
            //Vector3 offset = WobbleWithAmplitude(Time.time + i, m_XWobbleFinal, m_YWobbleFinal, amp);
            offset.x = amp * m_XWobbleFinal * Mathf.Sin(Time.time + i);
            offset.y = amp * m_YWobbleFinal * Mathf.Cos(Time.time + i);

            // Update vertices info and colours info
            vertices[i] += offset;
            colours[i].a = alpha;
        }

        // Update the actual vertices
        mesh.vertices = vertices;
        mesh.colors = colours;

        // Update the actual mesh
        m_TextMesh.UpdateGeometry(mesh, 0);

        m_PrevVerts = vertices;
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
}
