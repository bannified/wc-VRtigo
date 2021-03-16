using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTiles : MonoBehaviour
{
    [SerializeField]
    protected List<Image> m_ProgressTilesBgs;

    [SerializeField]
    protected List<Image> m_ProgressTiles;

    [SerializeField]
    protected float m_TileFadeInDuration = 0.2f;

    private int m_lastActivatedTileIdx = -1;

    void Start()
    {
        ResetProgress();
    }

    public void StartProgress()
    {
        ResetProgress();
        for (int i = 0; i < m_ProgressTilesBgs.Count; i++)
        {
            StartCoroutine("FadeInImage", m_ProgressTilesBgs[i]);
        }
    }

    public void CancelProgress()
    {
        for (int i = 0; i < m_ProgressTiles.Count; i++)
        {
            StartCoroutine("FadeOutImage", m_ProgressTiles[i]);
        }
        for (int i = 0; i < m_ProgressTilesBgs.Count; i++)
        {
            StartCoroutine("FadeOutImage", m_ProgressTilesBgs[i]);
        }
    }

    public void SetProgress(float progress)
    {
        int idxToActivate = (int)(m_ProgressTiles.Count * progress) - 1;
        if (idxToActivate > m_lastActivatedTileIdx)
        {
            m_lastActivatedTileIdx = idxToActivate;
            StartCoroutine("FadeInImage", m_ProgressTiles[idxToActivate]);
        }
    }

    private void ResetProgress()
    {

        for (int i = 0; i < m_ProgressTilesBgs.Count; i++)
        {
            Color imageColor = m_ProgressTilesBgs[i].color;
            imageColor.a = 0.0f;
            m_ProgressTilesBgs[i].color = imageColor;
        }

        for (int i = 0; i < m_ProgressTiles.Count; i++)
        {
            Color imageColor = m_ProgressTiles[i].color;
            imageColor.a = 0.0f;
            m_ProgressTiles[i].color = imageColor;
        }
        m_lastActivatedTileIdx = -1;
    }

    IEnumerator FadeInImage(Image img)
    {
        float durationSoFar = 0.0f;
        float progress = 0.0f;
        float alpha = 0.0f;

        while (durationSoFar < m_TileFadeInDuration)
        {
            progress = durationSoFar / m_TileFadeInDuration;
            alpha = progress + (Time.deltaTime / m_TileFadeInDuration);

            Color imageColor = img.color;
            imageColor.a = alpha;
            img.color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOutImage(Image img)
    {
        float durationSoFar = 0.0f;
        float progress = 0.0f;
        float oriAlpha = img.color.a;
        float alpha = oriAlpha;

        while (alpha > 0.0f || durationSoFar < m_TileFadeInDuration)
        {
            progress = durationSoFar / m_TileFadeInDuration;
            alpha = oriAlpha - progress - (Time.deltaTime / m_TileFadeInDuration);

            Color imageColor = img.color;
            imageColor.a = alpha;
            img.color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }
}
