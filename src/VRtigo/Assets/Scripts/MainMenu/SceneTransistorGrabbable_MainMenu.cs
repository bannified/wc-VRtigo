using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SceneTransistorGrabbable_MainMenu : MonoBehaviour, IGrabbable
{
    [SerializeField]
    protected RecordPlayer_MainMenu m_RecordPlayer;

    public bool fly = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fly)
        {
            m_RecordPlayer.SetDisc(this);
            fly = false;
        }
    }

    public void Grabbed() { }

    public void Dropped() { }
}
