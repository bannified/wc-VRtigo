using UnityEngine;
using TMPro;

public class WobbleTextEffect : MonoBehaviour
{
	// Parameters on how much it should wobble
	public float xWobble = 0.04f;
	public float yWobble = 0.01f;

	private TMP_Text textMesh;
	private Mesh mesh;
	private Vector3[] vertices;

	void Start()
	{
		this.textMesh = this.GetComponent<TMP_Text>();
	}

	void Update()
	{
		this.textMesh.ForceMeshUpdate();
		this.mesh = textMesh.mesh;
		this.vertices = mesh.vertices;

		// Calculate offset for each vertices in the text
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 offset = Wobble(Time.time + i);
			vertices[i] += offset;
		}

		// Update the actual vertices
		mesh.vertices = vertices;

		// Update the actual mesh
		textMesh.UpdateGeometry(mesh, 0);
	}

	/**
	 * Output a vector2 offset based on the given seed (time)
	 */
	private Vector2 Wobble(float time)
	{
		return new Vector2(Mathf.Sin(time * this.xWobble), Mathf.Cos(time * this.yWobble));
	}
}
