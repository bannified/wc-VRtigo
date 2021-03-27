using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIComponent
{
    /**
     * Set the UI component to be visible or invisible. UI component
     * should perform their necessary transition (fade in/fade out) here.
     */
    void SetVisible();
    void SetInvisible();

    /**
     * Set the UI Component active/disabled. When UI Component is disabled,
     * it should not be visible even when it is triggered.
     *
     * Typically used after player trigger an event/flag, where some UI component
     * will be enabled or disabled.
     *
     * Use case: After player finished the lesson, the 'Continue' UI component
     * of the continue button should no longer appear.
     */
    void SetActive();
    void SetDisable();
}