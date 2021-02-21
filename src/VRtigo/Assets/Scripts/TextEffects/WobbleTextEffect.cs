using UnityEngine;
using TMPro;

public static class WobbleTextEffect
{
	/**
	 * To apply, call this method within the Update() function.
	 */
	public static void ActivateWobbleEffect(TMP_Text textMesh, float xWobble, float yWobble)
	{
		textMesh.ForceMeshUpdate();
		Mesh mesh = textMesh.mesh;
		Vector3[] vertices = mesh.vertices;

		// Update the actual vertices
		mesh.vertices = WobbleVertices(vertices, xWobble, yWobble);

		// Update the actual mesh
		textMesh.UpdateGeometry(mesh, 0);
	}

	/**
	 * Wobble the given vertices. Mutate the given vector3[].
	 * Can be called separately from ActivateWobbleEffect to stack the effect.
	 */
	public static Vector3[] WobbleVertices(Vector3[] vertices, float xWobble, float yWobble)
	{
		// Calculate offset for each vertices in the text
		for (int i = 0; i < vertices.Length; i++)
		{
			Vector3 offset = Wobble(Time.time + i, xWobble, yWobble);
			vertices[i] += offset;
		}

		return vertices;
	}

	/**
	 * Output a vector2 offset based on the given seed.
	 */
	private static Vector2 Wobble(float seed, float xWobble, float yWobble)
	{
		return new Vector2(Mathf.Sin(seed * xWobble), Mathf.Cos(seed * yWobble));
	}
}
