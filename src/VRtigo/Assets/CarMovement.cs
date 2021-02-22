using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 0;
    public Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ih");
        transform.Translate(speed * direction * Time.deltaTime);
    }

    public void updateSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    public void updateDirection(Vector3 newDirection) 
    {
        direction = newDirection;
    }
}
