using UnityEngine;

public class Player : Singleton<Player>
{
	public Transform PlayerModel;
	public PlayerController PlayerController { get; private set; }
	public PlayerMovement PlayerMovement { get; private set; }
	public CubeController CubeController { get; private set; }

	private TrailRenderer trail;
	private Material trailMat;

	private void Awake()
	{
		PlayerController = GetComponent<PlayerController>();
		PlayerMovement = GetComponent<PlayerMovement>();
		CubeController = GetComponentInChildren<CubeController>();

		trail = GetComponentInChildren<TrailRenderer>();
		trailMat = trail.sharedMaterial;
	}

	private void OnEnable()
	{
		LevelManager.OnLevelStart += OnLevelStarted;
		LevelManager.OnLevelSuccess += OnLevelSucceed;
		LevelManager.OnLevelFail += OnLevelFailed;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelStart -= OnLevelStarted;
		LevelManager.OnLevelSuccess -= OnLevelSucceed;
		LevelManager.OnLevelFail -= OnLevelFailed;
	}

	private void OnLevelStarted()
	{
		PlayerController.CanPlay = true;
		PlayerMovement.CanMove = true;
	}

	private void OnLevelSucceed()
	{
		PlayerController.CanPlay = false;
		PlayerMovement.CanMove = false;
	}

	private void OnLevelFailed()
	{
		PlayerController.CanPlay = false;
		PlayerMovement.CanMove = false;
	}

	public void SetActiveTrail(bool isActive)
	{
		trail.emitting = isActive;
	}

	public void ChangeTrailColor(Color color)
	{
		trailMat.color = color;
	}
}