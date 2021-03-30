using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    /**
     * Set the UI component to be visible or invisible. UI component
     * should perform their necessary transition (e.g. fade in/fade out) here.
     */
    public virtual void SetVisible() { }

    public virtual void SetInvisible() { }

    /**
     * Set the UI Component active/disabled. When UI Component is disabled,
     * it should not be visible even when it is triggered.
     *
     * Typically used after player trigger an event/flag, where some UI component
     * will be enabled or disabled.
     * 
     * UIComponent should handle the enabled/disabled gracefully i.e. call their
     * transition accordingly.
     *
     * Use case: After player finished the lesson, the 'Continue' UI component
     * of the continue button should no longer appear.
     */
    public virtual void Enable() { }
    public virtual void Disable() { }
}
