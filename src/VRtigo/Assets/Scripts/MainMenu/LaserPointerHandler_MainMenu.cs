using UnityEngine;
using Valve.VR.Extras;

public class LaserPointerHandler_MainMenu : MonoBehaviour
{
	[SerializeField]
	protected SteamVR_LaserPointer m_LaserPointer;

	private void Awake()
	{
		// Register our own event handler
		m_LaserPointer.PointerIn += PointerIn;
		m_LaserPointer.PointerClick += PointerClick;
		m_LaserPointer.PointerOut += PointerOut;
	}

	private void Start()
	{
		m_LaserPointer = GetComponent<SteamVR_LaserPointer>();
		Debug.Assert(m_LaserPointer != null, "LaserPointerHandler_MainMenu requires SteamVR_LaserPointer on the same GameObject!");
	}

	public void PointerIn(object sender, PointerEventArgs e)
	{
		IActivatable activatableObj = e.target.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.OnHoverIn();
		}
	}

	public void PointerClick(object sender, PointerEventArgs e)
	{
		IActivatable activatableObj = e.target.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.Activate();
		}
	}

	public void PointerOut(object sender, PointerEventArgs e)
	{
		IActivatable activatableObj = e.target.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.OnHoverOut();
		}
	}
}
