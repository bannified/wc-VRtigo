using UnityEngine;
using TMPro;

public class FadeInWobbleTextEffect
{
    protected TMP_Text m_TextMesh;
    protected float m_Progress;

    private float m_XWobbleFinal;   // Extent of wobble in x direction after fade in finishes
    private float m_YWobbleFinal;   // Extent of wobble in y direction after fade in finishes
    private float m_FadeDuration;   // Total duration of fade in effect
    private float m_WobbleRange;    // During fade in, the extent of wobble will interpolate from (wobbleFinal + range) to wobbleFinal
    private float m_DurationSoFar;  // Duration of effect so far

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
        m_Progress = 0.0f;
    }

    public void Update()
    {
        if (m_DurationSoFar > m_FadeDuration) return;

        m_TextMesh.ForceMeshUpdate();
        Mesh mesh = m_TextMesh.mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colours = mesh.colors;

        // Update progress by tracking duration
        m_Progress = m_DurationSoFar / m_FadeDuration;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Extent of wobble decreases as progress increases
            float amp = 1.0f + (m_WobbleRange * (1.0f - m_Progress));
            // Since progress is [0, 1], we can use it for alpha too
            float alpha = m_Progress;
            Vector3 offset = WobbleWithAmplitude(Time.time + i, m_XWobbleFinal, m_YWobbleFinal, amp);

            // Update vertices info and colours info
            vertices[i] += offset;
            colours[i].a = alpha;
        }

        // Update the actual vertices
        mesh.vertices = vertices;
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

    /**
     * Output a vector2 offset based on the given seed.
     */
    private static Vector2 WobbleWithAmplitude(float seed, float xWobble, float yWobble, float amplify)
    {
        return new Vector2(Mathf.Sin(seed * xWobble * amplify), Mathf.Cos(seed * yWobble * amplify));
    }
}
