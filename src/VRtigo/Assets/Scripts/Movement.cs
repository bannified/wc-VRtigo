using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SpatialTracking;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // vector taken from joystick movement
    public SteamVR_Action_Vector2 movementDirInput;

    // taken from right and left trigger respectively
    public SteamVR_Action_Single rightRotationInput;
    public SteamVR_Action_Single leftRotationInput;

    // taken from B and A button respectively
    public SteamVR_Action_Single lookUpInput;
    public SteamVR_Action_Single lookDownInput;

    public SteamVR_Input_Sources rightHand;
    public SteamVR_Input_Sources leftHand;

    public float movementSpeed = 3.0f;
    public float rotationSpeed = 1.0f;
    public bool isCameraFixed = false;
    public bool enableDeathAnim = false;

    public GameObject headset;
    public Rigidbody playerRigidbody;
    public TrackedPoseDriver trackedPoseDriver;

    // processed vr inputs
    public Vector2 movementDir;

    void Start()
    {
        movementDirInput.AddOnAxisListener(ProcessMovementDirInput, rightHand);
        rightRotationInput.AddOnAxisListener(ProcessRightRotationInput, rightHand);
        leftRotationInput.AddOnAxisListener(ProcessLeftRotationInput, leftHand);
        lookUpInput.AddOnAxisListener(ProcessLookUpInput, rightHand);
        lookDownInput.AddOnAxisListener(ProcessLookDownInput, rightHand);

        playerRigidbody = this.GetComponent<Rigidbody>();
        trackedPoseDriver = headset.GetComponent<TrackedPoseDriver>();

        // restrict camera rotation with headset
        if (isCameraFixed) 
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.PositionOnly;
        } 
        else 
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
    }

    private void FixedUpdate() 
    {
        Vector3 playerMovementDir = headset.transform.forward * movementDir.y + headset.transform.right * movementDir.x;
        playerMovementDir.y = 0;
        playerMovementDir.Normalize();

        //Debug.Log(playerMovementDir.sqrMagnitude);

        if (movementDir.sqrMagnitude > 0.05f)
        {
            playerRigidbody.velocity = movementSpeed * playerMovementDir;
        }
        else 
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Car")) {
            Debug.Log("You died");
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.PositionOnly;   // stop headset rotation
            playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            if (enableDeathAnim) 
            {
                // Todo: Add head spinning maybe?
            }
            FlashDeathUI();
        }
    }

    void FlashDeathUI() 
    {

        Text ui = GameObject.Find("Death Screen/Text").GetComponent<Text>();
        Color color = ui.color;
        color.a = 1.0f;
        ui.color = color;
    }

    // handles vector input from right joystick
    public void ProcessMovementDirInput(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) 
    {
        movementDir = axis;
    }

    public void ProcessRightRotationInput(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) 
    {
        if (isCameraFixed && newAxis > 0) 
        {
            headset.transform.Rotate(new Vector3(0, rotationSpeed, 0));
        }
    }

    public void ProcessLeftRotationInput(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) 
    {
        if (isCameraFixed && newAxis > 0) 
        {
            headset.transform.Rotate(new Vector3(0, -rotationSpeed, 0));
        }
    }

    public void ProcessLookUpInput(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) 
    {
        if (isCameraFixed && newAxis > 0) 
        {
            headset.transform.Rotate(new Vector3(rotationSpeed, 0, 0));
        }
    }

    public void ProcessLookDownInput(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta) 
    {
        if (isCameraFixed && newAxis > 0) 
        {
            headset.transform.Rotate(new Vector3(-rotationSpeed, 0, 0));
        }
    }
}
