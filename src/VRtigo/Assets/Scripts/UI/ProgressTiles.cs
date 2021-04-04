using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTiles : UIComponent
{
    [SerializeField]
    private SoundData m_TileActivateSound;

    [SerializeField]
    private float m_TilePitchStep = 0.2f;

    [SerializeField]
    protected List<Image> m_ProgressTilesBgs;

    [SerializeField]
    protected List<Image> m_ProgressTiles;

    [SerializeField]
    protected float m_TileFadeDur = 0.2f;

    private List<float> m_TileActivateSoundPitches;
    private int m_LastActivatedTileIdx = -1;
    private bool m_IsEnabled = true;

    void Start()
    {
        ResetProgress();

        AudioManager.InitAudioSourceOn(m_TileActivateSound, this.gameObject);

        m_TileActivateSoundPitches = new List<float>();
        for (int i = 0; i < m_ProgressTiles.Count; i++)
            m_TileActivateSoundPitches.Add(m_TileActivateSound.pitch + i * m_TilePitchStep);
    }

    public override void Enable()
    {
        m_IsEnabled = true;
        SetVisible();
    }

    public override void Disable()
    {
        SetInvisible();
        m_IsEnabled = false;
    }

    public override void SetVisible()
    {
        if (m_IsEnabled)
        {
            ResetProgress();
            for (int i = 0; i < m_ProgressTilesBgs.Count; i++)
            {
                StartCoroutine(Image_Utils.FadeInImageCoroutine(m_ProgressTilesBgs[i], m_TileFadeDur));
            }
        }
    }

    public override void SetInvisible()
    {
        for (int i = 0; i < m_ProgressTiles.Count; i++)
        {
            StartCoroutine(Image_Utils.FadeOutImageCoroutine(m_ProgressTiles[i], m_TileFadeDur));
        }
        for (int i = 0; i < m_ProgressTilesBgs.Count; i++)
        {
            StartCoroutine(Image_Utils.FadeOutImageCoroutine(m_ProgressTilesBgs[i], m_TileFadeDur));
        }
    }

    public void SetProgress(float progress)
    {
        if (m_IsEnabled)
        {
            int idxToActivate = (int)(m_ProgressTiles.Count * progress) - 1;
            if (idxToActivate > m_LastActivatedTileIdx)
            {
                m_LastActivatedTileIdx = idxToActivate;

                // Play SFX
                m_TileActivateSound.m_Source.pitch = m_TileActivateSoundPitches[idxToActivate];
                m_TileActivateSound.m_Source.Play();

                // Fade in progress tile
                StartCoroutine(Image_Utils.FadeInImageCoroutine(m_ProgressTiles[idxToActivate], m_TileFadeDur));
            }
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
        m_LastActivatedTileIdx = -1;
    }
}
