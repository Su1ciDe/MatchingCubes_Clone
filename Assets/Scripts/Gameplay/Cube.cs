using DG.Tweening;
using UnityEngine;

public class Cube : MonoBehaviour
{
	public bool IsInStack { get; set; }

	public CubeColor CubeColor { get; private set; }
	public Material Material { get; private set; }
	public  MeshRenderer MeshRenderer { get; private set; }

	private void Awake()
	{
		MeshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	public void ChangeColor(CubeColor cubeColor, Material material)
	{
		CubeColor = cubeColor;
		MeshRenderer.sharedMaterial = material;
		Material = material;
	}

	private void OnDestroy()
	{
		transform.DOKill();
	}
}