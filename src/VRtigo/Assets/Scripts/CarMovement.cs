using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public SoundData m_CartHitSound;
    public SoundData m_CartDrivingSound;
    public float speed = 0;
    public Vector3 direction = Vector3.zero;

    void Start()
    {
        if (m_CartDrivingSound != null)
        {
            AudioManager.InitAudioSourceOn(m_CartDrivingSound, this.gameObject);

            m_CartDrivingSound.m_Source.Play();
        }

        if (m_CartHitSound != null)
            AudioManager.InitAudioSourceOn(m_CartHitSound, this.gameObject);
    }

    void Update()
    {
        transform.Translate(speed * direction * Time.deltaTime);
    }

    public void UpdateSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void UpdateDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void OnPlayerHit()
    {
        if (m_CartHitSound != null)
            m_CartHitSound.m_Source.Play();
    }
}
