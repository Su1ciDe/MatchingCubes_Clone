using UnityEngine;

public class Player : Singleton<Player>
{
	public static int Diamond
	{
		get => PlayerPrefs.GetInt("Diamond", 0);
		set => PlayerPrefs.SetInt("Diamond", value);
	}

	public Transform PlayerModel;
	public PlayerController PlayerController { get; private set; }
	public PlayerMovement PlayerMovement { get; private set; }
	public CubeController CubeController { get; private set; }

	[Header("Trail")]
	[SerializeField] private TrailRenderer blueTrail;
	[SerializeField] private TrailRenderer orangeTrail;
	[SerializeField] private TrailRenderer purpleTrail;
	public TrailRenderer Trail { get; private set; }

	private void Awake()
	{
		PlayerController = GetComponent<PlayerController>();
		PlayerMovement = GetComponent<PlayerMovement>();
		CubeController = GetComponentInChildren<CubeController>();
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
		Trail.emitting = isActive;
	}

	public void ChangeTrailColor(CubeColor color)
	{
		blueTrail.emitting = false;
		orangeTrail.emitting = false;
		purpleTrail.emitting = false;

		Trail = color switch
		{
			CubeColor.Blue => blueTrail,
			CubeColor.Orange => orangeTrail,
			CubeColor.Purple => purpleTrail,
			_ => Trail
		};

		Trail.emitting = true;
	}
}