using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleIconTrigger : MonoBehaviour
{
    [SerializeField]
    protected Image m_CircleIcon;

    public bool m_IsVisible = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Always rotate to camera
    }

    private void OnTriggerEnter(Collider other)
    {
        // Fade In Icon
    }

    private void OnTriggerExit(Collider other)
    {
        // Fade Out Icon

    }

    public void DeactivateCircleIcon()
    {
        m_IsVisible = false;

        // Fade out
    }
}
