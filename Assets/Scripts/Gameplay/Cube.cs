using UnityEngine;

public class Cube : MonoBehaviour
{
	public bool IsInStack { get; set; }

	public CubeColor CubeColor { get; private set; }
	public Color MaterialColor { get; private set; }

	private MeshRenderer meshRenderer;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	public void ChangeColor(CubeColor cubeColor, Material material)
	{
		CubeColor = cubeColor;
		meshRenderer.sharedMaterial = material;
		MaterialColor = material.color;
	}
}