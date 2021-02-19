using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Movement : MonoBehaviour
{
    // vector taken from joystick movement
    public SteamVR_Action_Vector2 movementDirInput;

    // a reference to the hand
    public SteamVR_Input_Sources handType;

    public float movementSpeed = 3.0f;
    public GameObject headset;
    public Rigidbody playerRigidbody;

    // processed vr inputs
    public Vector2 movementDir;

    void Start()
    {
        movementDirInput.AddOnAxisListener(ProcessMovementDirInput, handType);
        playerRigidbody = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        Vector3 playerMovementDir = headset.transform.forward * movementDir.y + headset.transform.right * movementDir.x;
        playerMovementDir.y = 0;
        playerMovementDir.Normalize();

        //Debug.Log(playerMovementDir.sqrMagnitude);

        if (movementDir.sqrMagnitude > 0.05f) {
            playerRigidbody.velocity = movementSpeed * playerMovementDir;
        }
        else {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    // Handles vector input from right joystick
    public void ProcessMovementDirInput(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) 
    {
        movementDir = axis;
    }
}
