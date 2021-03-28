using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTrigger : UIComponent
{
    [SerializeField]
    private Grabbable m_GrabbableObj;

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
            m_GrabbableObj.onGrab += SetDisableOnGrabbable;
            m_GrabbableObj.onDrop += SetActiveOnGrabbable;

            m_GrabbableObj.onNonInteractable += SetDisableOnGrabbable;
            m_GrabbableObj.OnInteractable += SetActiveOnGrabbable;
        }
    }

    private void OnDisable()
    {
        if (m_GrabbableObj != null)
        {
            m_GrabbableObj.onGrab -= SetDisableOnGrabbable;
            m_GrabbableObj.onDrop -= SetActiveOnGrabbable;

            m_GrabbableObj.onNonInteractable -= SetDisableOnGrabbable;
            m_GrabbableObj.OnInteractable -= SetActiveOnGrabbable;
        }
    }

    // Wrappers
    private void SetActiveOnGrabbable(Grabbable grabbable) { SetEnable(); }
    private void SetDisableOnGrabbable(Grabbable grabbable) { SetDisable(); }

    public override void SetEnable()
    {
        m_IsEnabled = true;
        if (m_IsWithinBoundary)
            SetVisible();
    }

    public override void SetDisable()
    {
        m_IsEnabled = false;
        if (m_IsWithinBoundary)
            SetInvisible();
    }

    public override void SetVisible()
    {
        // Fade In Icon
        StartCoroutine(Image_Utils.FadeInImageCoroutine(m_Icon, m_FadeDuration));

        // Face to camera
        m_FaceCamCoroutine = StartCoroutine(Image_Utils.FaceToCameraCoroutine(m_Icon, m_Camera));
    }

    public override void SetInvisible()
    {
        // Fade Out Icon
        StartCoroutine(Image_Utils.FadeOutImageCoroutine(m_Icon, m_FadeDuration));

        // Stop face to camera coroutine
        if (m_FaceCamCoroutine != null)
        {
            StopCoroutine(m_FaceCamCoroutine);
            m_FaceCamCoroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            SetVisible();

        m_IsWithinBoundary = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_IsEnabled && m_TagsThatActivate.Contains(other.gameObject.tag))
            SetInvisible();

        m_IsWithinBoundary = false;
    }
}
