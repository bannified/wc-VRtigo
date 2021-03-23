using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTrigger : MonoBehaviour
{
    [SerializeField]
    protected Grabbable m_GrabbableObj;

    [SerializeField]
    protected Image m_Icon;

    [SerializeField]
    protected float m_FadeDuration = 0.2f;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "Player" };

    [SerializeField]
    protected Camera m_Camera;

    private Coroutine m_FaceCamCoroutine;

    private bool m_IsWithinBoundary = false; // Tracks whether Player is currently within boundary
    private bool m_IsEnabled = true; // Tracks whether Icon should be shown or not

    // Start is called before the first frame update
    void Start()
    {
        if (m_Camera == null)
            m_Camera = Camera.main;

        // Set alpha to 0
        Color imageColor = m_Icon.color;
        imageColor.a = 0.0f;
        m_Icon.color = imageColor;
    }

    private void OnEnable()
    {
        if (m_GrabbableObj != null)
        {
            m_GrabbableObj.onGrab += DisableIcon;
            m_GrabbableObj.onDrop += EnableIcon;

            m_GrabbableObj.onNonInteractable += DisableIcon;
            m_GrabbableObj.OnInteractable += EnableIcon;
        }
    }

    private void OnDisable()
    {
        if (m_GrabbableObj != null)
        {
            m_GrabbableObj.onGrab -= DisableIcon;
            m_GrabbableObj.onDrop -= EnableIcon;

            m_GrabbableObj.onNonInteractable -= DisableIcon;
            m_GrabbableObj.OnInteractable -= EnableIcon;
        }
    }

    /**
     * Once enabled, Icon will appear when player is within trigger boundary
     */
    public void EnableIcon(Grabbable grabObj)
    {
        m_IsEnabled = true;
        if (m_IsWithinBoundary)
            ShowIcon();
    }

    /**
     * Once disabled, Icon will not appear even when player is within trigger boundary.
     */
    public void DisableIcon(Grabbable grabObj)
    {
        m_IsEnabled = false;
        if (m_IsWithinBoundary)
            FadeOutIcon();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            ShowIcon();

        m_IsWithinBoundary = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            FadeOutIcon();

        m_IsWithinBoundary = false;
    }

    /**
     * Fade in the icon and make it always face the camera.
     */
    public void ShowIcon()
    {
        // Fade In Icon
        StartCoroutine("FadeInImage", m_Icon);

        // Face to camera
        m_FaceCamCoroutine = StartCoroutine(FaceToCamera(m_Icon));
    }

    /**
     * Fade out the icon.
     */
    public void FadeOutIcon()
    {
        // Fade Out Icon
        StartCoroutine("FadeOutImage", m_Icon);

        // Stop face to camera coroutine
        if (m_FaceCamCoroutine != null)
        {
            StopCoroutine(m_FaceCamCoroutine);
            m_FaceCamCoroutine = null;
        }
    }

    IEnumerator FaceToCamera(Image img)
    {
        while (true)
        {
            Vector3 camForward = m_Camera.transform.forward;
            camForward.y = 0.0f;

            img.transform.rotation = Quaternion.LookRotation(camForward);
            yield return null;
        }
    }

    IEnumerator FadeInImage(Image img)
    {
        float durationSoFar = 0.0f;
        float progress, alpha;

        while (durationSoFar < m_FadeDuration)
        {
            progress = durationSoFar / m_FadeDuration;
            alpha = progress + (Time.deltaTime / m_FadeDuration);

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
        float oriAlpha = img.color.a;
        float alpha = oriAlpha;
        float progress;

        while (alpha > 0.0f || durationSoFar < m_FadeDuration)
        {
            progress = durationSoFar / m_FadeDuration;
            alpha = oriAlpha - progress - (Time.deltaTime / m_FadeDuration);

            Color imageColor = img.color;
            imageColor.a = alpha;
            img.color = imageColor;

            durationSoFar += Time.deltaTime;
            yield return null;
        }
    }
}
