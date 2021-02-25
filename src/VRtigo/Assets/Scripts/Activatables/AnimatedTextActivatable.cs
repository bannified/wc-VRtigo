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
    public float m_DisplayDuration = 5.0f;
    public float m_ScatterRange = 10.0f;

    private string m_Text;

    private FadeInWobbleTextEffect m_FadeInEffect;
    private FadeOutScatterTextEffect m_ScatterEffect;

    private bool m_IsAnimating = false;
    private bool m_IsFadingIn = false;
    private bool m_IsFadingOut = false;

    public bool start = false;

    void Start()
    {
        m_TextMesh = GetComponentInChildren<TMP_Text>();
        m_Text = m_TextMesh.text;
        m_TextMesh.SetText("");

        m_FadeInEffect = new FadeInWobbleTextEffect(m_TextMesh, m_XWobble, m_YWobble, m_FadeInDuration, m_InitWobbleRange);
        m_ScatterEffect = new FadeOutScatterTextEffect(m_TextMesh, m_ScatterRange, m_FadeOutDuration);
    }

    void Update()
    {
        if (start)
        {
            Activate();
        }

        if (m_IsFadingIn)
        {
            m_FadeInEffect.Update();
        }
        else if (m_IsFadingOut)
        {
            m_ScatterEffect.Update();
        }
    }

    public void Activate()
    {
        if (!m_IsAnimating)
            StartCoroutine("AnimateText");
    }

    IEnumerator AnimateText()
    {
        m_IsAnimating = true;
        m_TextMesh.SetText(m_Text);
        m_TextMesh.ForceMeshUpdate();

        m_IsFadingIn = true;

        yield return new WaitForSeconds(m_FadeInDuration + m_DisplayDuration);

        m_ScatterEffect.SetText(m_TextMesh);
        m_ScatterEffect.SetVerticesPosition(m_FadeInEffect.GetVerticesPosition());
        m_IsFadingIn = false;
        m_IsFadingOut = true;

        yield return new WaitForSeconds(m_FadeOutDuration);

        m_IsFadingOut = false;
        m_IsAnimating = false;
    }
}
