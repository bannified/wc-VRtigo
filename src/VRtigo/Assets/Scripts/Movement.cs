using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Movement : MonoBehaviour
{
    // a reference to the action
    public SteamVR_Action_Vector2 movementDirection;
    // a reference to the hand
    public SteamVR_Input_Sources handType;
    public float movementSpeed = 0.03f;

    void Start()
    {
        movementDirection.AddOnAxisListener(Move, handType);
    }

    void update() 
    {

    }

    // Handles vector input from right joystick
    public void Move(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) {
        //Axis gives direction
        Debug.Log("Axis: " + axis + ", Delta: " + delta);
        Vector3 v = new Vector3(axis.x, 0, axis.y);
        transform.Translate(v * movementSpeed);
    }
}
