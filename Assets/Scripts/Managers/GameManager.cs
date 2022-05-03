using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public static Camera MainCamera;
	public Material BlueMaterial;
	public Material OrangeMaterial;
	public Material PurpleMaterial;
	public Material MatchedMaterial;

	private void Awake()
	{
		MainCamera = Camera.main;
	}
}