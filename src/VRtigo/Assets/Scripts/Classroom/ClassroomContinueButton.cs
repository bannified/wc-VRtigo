using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomContinueButton : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected Sound m_ButtonSound;

    [SerializeField]
    private List<string> m_TagsThatActivate = new List<string> { "PlayerHands" };

    [SerializeField]
    private float m_ButtonSpeed = 2.0f;

    [SerializeField]
    private float m_ButtonDisplacement = -0.0457f;

    void Start()
    {
        AudioManager.InitAudioSourceOn(m_ButtonSound, this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_TagsThatActivate.Contains(other.gameObject.tag))
        {
            Activate();
        }
    }

    public void Activate()
    {
        Debug.Log("Activate");
        // Play button sound
        m_ButtonSound.m_Source.Play();

        // Play animation
        StartCoroutine("PressButton", new Vector3(0, m_ButtonDisplacement, 0));

        // Proceed to next step
        ClassroomManager.Instance.NextStep();
    }

    IEnumerator PressButton(Vector3 displacement)
    {
        // Press
        yield return StartCoroutine("MoveButton", displacement);
        
        // Back to original position
        yield return StartCoroutine("MoveButton", -displacement);
    }

    IEnumerator MoveButton(Vector3 displacement)
    {
        Vector3 targetPos = transform.position + displacement;
        Vector3 offset;

        while (!Mathf.Approximately((targetPos - transform.position).magnitude, 0.0f))
        {
            offset = (targetPos - transform.position) * Time.deltaTime * m_ButtonSpeed;
            transform.position += offset;
            yield return null;
        }
    }
}
