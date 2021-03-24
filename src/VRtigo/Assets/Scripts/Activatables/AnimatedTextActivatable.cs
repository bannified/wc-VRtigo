using System.Collections;
using UnityEngine;
using TMPro;

public class AnimatedTextActivatable : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected TMP_Text m_TextMesh;

    [Header("Settings")]
    public float m_XWobble = 0.05f;
    public float m_YWobble = 0.01f;
    public float m_InitWobbleRange = 2.0f;
    public float m_FadeInDuration = 3.0f;
    public float m_FadeOutDuration = 3.0f;
    public float m_ScatterRange = 10.0f;

    private string m_Text;

    private FadeInWobbleTextEffect m_FadeInEffect;
    private FadeOutScatterTextEffect m_ScatterEffect;

    private bool m_IsAnimating = false;
    private bool m_StopAnimating = false;

    void Start()
    {
        m_Text = m_TextMesh.text;
        m_TextMesh.SetText("");

        m_FadeInEffect = new FadeInWobbleTextEffect(m_TextMesh, m_XWobble, m_YWobble, m_FadeInDuration, m_InitWobbleRange);
        m_ScatterEffect = new FadeOutScatterTextEffect(m_TextMesh, m_ScatterRange, m_FadeOutDuration);
    }

    public void Activate()
    {
        if (!m_IsAnimating)
            StartCoroutine("AnimateText");
    }

    public void Deactivate()
    {
        m_StopAnimating = true;
    }

    IEnumerator AnimateText()
    {
        float durationSoFar = 0.0f;
        float totalDur = m_FadeInDuration;

        m_StopAnimating = false;
        m_IsAnimating = true;

        m_FadeInEffect.Reset();
        m_ScatterEffect.Reset();

        m_TextMesh.SetText(m_Text);
        m_TextMesh.ForceMeshUpdate();

        // Fade In effect
        while (durationSoFar <= totalDur)
        {
            m_FadeInEffect.Update();
            durationSoFar += Time.deltaTime;
            yield return null;
        }

        // Keep updating FadeInEffect on each frame as long as needed
        while (!m_StopAnimating)
        {
            m_FadeInEffect.Update();
            yield return null;
        }

        durationSoFar = 0.0f;
        totalDur = m_FadeOutDuration;

        m_ScatterEffect.SetText(m_TextMesh);
        m_ScatterEffect.SetVerticesPosition(m_FadeInEffect.GetVerticesPosition());

        // Keep updating ScatterEffect on each frame
        while (durationSoFar <= totalDur)
        {
            m_ScatterEffect.Update();
            durationSoFar += Time.deltaTime;
            yield return null;
        }

        m_IsAnimating = false;
    }
}
