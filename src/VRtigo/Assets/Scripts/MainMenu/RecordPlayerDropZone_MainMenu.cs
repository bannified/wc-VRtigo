using UnityEngine;

public class RecordPlayerDropZone_MainMenu : MonoBehaviour
{
    [SerializeField]
    protected RecordPlayer_MainMenu m_RecordPlayer;

    private void OnTriggerStay(Collider other)
    {
        SceneTransistorGrabbable_MainMenu disc = other.gameObject.GetComponent<SceneTransistorGrabbable_MainMenu>();
        bool result = disc != null && !disc.GetIsGrabbed() && m_RecordPlayer.SetDisc(disc);
    }
}
