using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTiles : MonoBehaviour
{
    [SerializeField]
    protected List<Image> m_ProgressTiles;

    [SerializeField]
    protected float m_TileFadeInDuration = 0.2f;

    private int m_lastActivatedTileIdx = -1;

    void Start()
    {
        for (int i = 0; i < m_ProgressTiles.Count; i++)
        {
            Color imageColor = m_ProgressTiles[i].color;
            imageColor.a = 0.0f;
            m_ProgressTiles[i].color = imageColor;
        }
    }

    public void SetProgress(float progress)
    {
        int idxToActivate = (int)(m_ProgressTiles.Count * progress) - 1;
        if (idxToActivate > m_lastActivatedTileIdx)
        {
            m_lastActivatedTileIdx = idxToActivate;
            StartCoroutine("FadeTileIn", idxToActivate);
        }
    }

    IEnumerator FadeTileIn(int idx)
    {
        float durationSoFar = 0.0f;
        float progress = 0.0f;
        float alpha = 0.0f;

        while (durationSoFar < m_TileFadeInDuration)
        {
            progress = durationSoFar / m_TileFadeInDuration;
            alpha = progress + (Time.deltaTime / m_TileFadeInDuration);

            Color imageColor = m_ProgressTiles[idx].color;
            imageColor.a = alpha;
            m_ProgressTiles[idx].color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }
}
