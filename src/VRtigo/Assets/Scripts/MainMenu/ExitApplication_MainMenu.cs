using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitApplication_MainMenu : MonoBehaviour
{
    [SerializeField]
    private PlayerController_MainMenu m_PlayerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_PlayerController = other.gameObject.GetComponent<PlayerController_MainMenu>();
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
