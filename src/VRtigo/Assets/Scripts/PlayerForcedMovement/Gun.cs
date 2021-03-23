using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Gun : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private Transform m_MuzzlePoint;
    [SerializeField] private LayerMask m_TargetLayer;
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private LineRenderer m_Laser;
    
    [SerializeField] private float m_Range;
    [SerializeField] private float m_FireRate;
    private float m_FireCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Laser.SetPosition(0, m_MuzzlePoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the gun firing cooldown
        if (m_FireCooldown >= 0)
        {
            m_FireCooldown -= Time.deltaTime;
        }

        // Temporary input to fire the gun
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        // Update the laser sight
        RaycastHit hit;

        if (Physics.Raycast(m_MuzzlePoint.position, m_MuzzlePoint.forward, out hit))
        {
            if (hit.collider)
            {
                m_Laser.SetPosition(1, m_MuzzlePoint.position + new Vector3(0f, 0f, hit.distance));
            }
        }
        else
        {
            m_Laser.SetPosition(1, m_MuzzlePoint.position + new Vector3(0f, 0f, 500f));
        }
    }

    private void Shoot()
    {
        // Sets the rate of fire of the gun
        if (m_FireCooldown > 0)
        {
            return;
        }
        m_FireCooldown = 1 / m_FireRate;

        m_AudioSource.Play();

        // Spawn the projectile and delete it after some time
        GameObject bulletInstance = Instantiate(m_BulletPrefab, m_MuzzlePoint.position, m_MuzzlePoint.rotation);
        Destroy(bulletInstance, 1f);

        // Check if raycast hits the target
        RaycastHit hit;
        if (Physics.Raycast(m_MuzzlePoint.position, m_MuzzlePoint.forward, out hit, m_Range, m_TargetLayer))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TargetHit();
            }
        }
    }
}
