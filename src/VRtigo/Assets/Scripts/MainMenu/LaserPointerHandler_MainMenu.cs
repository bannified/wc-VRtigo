using UnityEngine;
using Valve.VR.Extras;
using Valve.VR;

public class LaserPointerHandler_MainMenu : MonoBehaviour
{
	public SteamVR_Behaviour_Pose pose;

	public SteamVR_Action_Boolean m_ActivateAction;

	public bool active = true;
	public Color color;
	public float thickness = 0.002f;
	public Color clickColor = Color.green;
	public Color grabColor = Color.red;
	public GameObject holder;
	public GameObject pointer;
	bool isActive = false;
	public bool addRigidBody = false;
	public Transform reference;

	private Transform previousContact = null;

	private void Start()
	{
		if (pose == null)
			pose = this.GetComponent<SteamVR_Behaviour_Pose>();
		if (pose == null)
			Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

		if (m_ActivateAction == null)
			Debug.LogError("No Activate action has been set on this component.", this);

		holder = new GameObject();
		holder.transform.parent = this.transform;
		holder.transform.localPosition = Vector3.zero;
		holder.transform.localRotation = Quaternion.identity;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
		pointer.transform.parent = holder.transform;
		pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
		pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
		pointer.transform.localRotation = Quaternion.identity;
		BoxCollider collider = pointer.GetComponent<BoxCollider>();
		if (addRigidBody)
		{
			if (collider)
			{
				collider.isTrigger = true;
			}
			Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
			rigidBody.isKinematic = true;
		}
		else
		{
			if (collider)
			{
				Object.Destroy(collider);
			}
		}
		Material newMaterial = new Material(Shader.Find("Unlit/Color"));
		newMaterial.SetColor("_Color", color);
		pointer.GetComponent<MeshRenderer>().material = newMaterial;
	}

	private void Update()
	{
		if (!isActive)
		{
			isActive = true;
			this.transform.GetChild(0).gameObject.SetActive(true);
		}

		float dist = 100f;

		Ray raycast = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		bool bHit = Physics.Raycast(raycast, out hit);

		if (previousContact && previousContact != hit.transform)
		{
			PointerEventArgs args = new PointerEventArgs();
			args.fromInputSource = pose.inputSource;
			args.distance = 0f;
			args.flags = 0;
			args.target = previousContact;
			OnPointerOut(args);
			previousContact = null;
		}
		if (bHit && previousContact != hit.transform)
		{
			PointerEventArgs argsIn = new PointerEventArgs();
			argsIn.fromInputSource = pose.inputSource;
			argsIn.distance = hit.distance;
			argsIn.flags = 0;
			argsIn.target = hit.transform;
			OnPointerIn(argsIn);
			previousContact = hit.transform;
		}
		if (!bHit)
		{
			previousContact = null;
		}
		if (bHit && hit.distance < 100f)
		{
			dist = hit.distance;
		}

		if (bHit && m_ActivateAction.GetStateUp(pose.inputSource))
		{
			PointerEventArgs argsClick = new PointerEventArgs();
			argsClick.fromInputSource = pose.inputSource;
			argsClick.distance = hit.distance;
			argsClick.flags = 0;
			argsClick.target = hit.transform;
			OnPointerClick(argsClick);
		}

		if (m_ActivateAction != null && m_ActivateAction.GetState(pose.inputSource))
		{
			pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
			pointer.GetComponent<MeshRenderer>().material.color = clickColor;
		}
		else
		{
			pointer.transform.localScale = new Vector3(thickness, thickness, dist);
			pointer.GetComponent<MeshRenderer>().material.color = color;
		}
		pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
	}

	public void OnPointerIn(PointerEventArgs e) 
	{
		IActivatable activatableObj = e.target.root.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.OnHoverIn();
		}
	}

	public void OnPointerClick(PointerEventArgs e) 
	{
		IActivatable activatableObj = e.target.root.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.Activate();
		}
	}

	public void OnPointerOut(PointerEventArgs e) 
	{
		IActivatable activatableObj = e.target.root.GetComponent<IActivatable>();
		if (activatableObj != null)
		{
			activatableObj.OnHoverOut();
		}
	}

	public struct PointerEventArgs
	{
		public SteamVR_Input_Sources fromInputSource;
		public uint flags;
		public float distance;
		public Transform target;
	}
}
