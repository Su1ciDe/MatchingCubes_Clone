using UnityEngine;

public class CollectableCube : Collectable
{
	public CubeColor CubeColor;

	public Material Material { get; private set; }

	private MeshRenderer meshRenderer;

	private void Awake()
	{
		meshRenderer = GetComponentInChildren<MeshRenderer>();
	}

	private void OnValidate()
	{
		if (!GameManager.Instance) return;
		// Change color in inspector
		meshRenderer = GetComponentInChildren<MeshRenderer>();
		var sharedMaterial = meshRenderer.sharedMaterial;
		sharedMaterial = CubeColor switch
		{
			CubeColor.Blue => GameManager.Instance.BlueMaterial,
			CubeColor.Orange => GameManager.Instance.OrangeMaterial,
			CubeColor.Purple => GameManager.Instance.PurpleMaterial,
			_ => sharedMaterial
		};
		meshRenderer.sharedMaterial = sharedMaterial;

		Material = sharedMaterial;
	}

	public override void OnCollect(Player collectorPlayer)
	{
		collectorPlayer.CubeController.AddCube(this);
		Destroy(gameObject);
	}
}