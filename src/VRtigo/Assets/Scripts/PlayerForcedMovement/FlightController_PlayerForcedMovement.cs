using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController_PlayerForcedMovement : MonoBehaviour
{
    [SerializeField] private Transform m_FlightPathContainer;

    [SerializeField] private float m_FlightSpeed;
    [SerializeField] private float m_TurnSpeed;
    [SerializeField] private float m_ThresholdDist; // Distance to waypoint before the next waypoint is chosen

    [SerializeField] private Vector3[] m_FlightPath;
    private int m_NextIndex; // Next waypoint index in the flight path
    private Vector3 m_NextWaypoint; // Position of next waypoint
    private bool m_FlightUnderway;

    private void Awake()
    {
        InitFlightPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (m_FlightUnderway)
        {
            MoveToNextWaypoint();
        }
    }

    private void InitFlightPath()
    {
        m_FlightPath = new Vector3[m_FlightPathContainer.childCount];
        int count = 0;
        foreach (Transform child in m_FlightPathContainer)
        {
            m_FlightPath[count++] = child.position;
        }
        if (m_FlightPath.Length > 0)
        {
            m_NextIndex = 1;
            m_NextWaypoint = m_FlightPath[m_NextIndex];
            m_FlightUnderway = true;
            transform.position = m_FlightPath[0];
            transform.rotation = Quaternion.LookRotation((m_NextWaypoint - transform.position).normalized);
        }
    }

    private void MoveToNextWaypoint()
    {
        Vector3 direction = m_NextWaypoint - transform.position;
        Vector3 dir = direction.normalized;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_TurnSpeed);
        transform.position += transform.forward * m_FlightSpeed * Time.deltaTime;

        // Update the next waypoint if destination reached
        if (direction.magnitude <= m_ThresholdDist)
        {
            m_NextIndex++;
            if (m_NextIndex == m_FlightPath.Length)
            {
                EndOfFlight();
            }
            else
            {
                m_NextWaypoint = m_FlightPath[m_NextIndex];
            }
        }
    }

    private void EndOfFlight()
    {
        m_FlightUnderway = false;
    }
}
