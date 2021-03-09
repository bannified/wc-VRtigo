using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float m_BulletSpeed = 500f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * m_BulletSpeed;
    }
}
