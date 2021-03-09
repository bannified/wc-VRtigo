using UnityEngine;

public class MainMenuSceneTransitionActivator : MonoBehaviour, IActivatable
{
    [SerializeField]
    protected SceneTransistor m_SceneTransistor;

    private string m_SceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Character character = other.gameObject.GetComponent<Character>();
            Character_MainMenu casted = character as Character_MainMenu;

            if (casted != null)
            {
                GameObject[] gameObjs = casted.GetGrabbedObjects();
                SceneTransistorGrabbable sceneTransistorObj = null;

                // Prioritise right hand object
                if (gameObjs[1] != null)
                {
                    sceneTransistorObj = gameObjs[1].GetComponent<SceneTransistorGrabbable>();
                } 
                else if (gameObjs[0] != null)
                {
                    sceneTransistorObj = gameObjs[0].GetComponent<SceneTransistorGrabbable>();
                } 
                else
                {
                    // TODO: Quit application pop up
                }

                if (sceneTransistorObj != null)
                {
                    m_SceneName = sceneTransistorObj.GetSceneName();
                    Activate();
                }
            } 
        }
    }

    public void Activate()
    {
        if (m_SceneName != null || m_SceneName != "")
        {
            m_SceneTransistor.FadeToScene(m_SceneName);
        }
    }
}
