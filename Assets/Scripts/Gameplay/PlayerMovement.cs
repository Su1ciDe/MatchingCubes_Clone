using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool IsFeverMode { get; set; }

	public bool CanMove { get; set; } = true;

	public Rigidbody Rb { get; private set; }

	[SerializeField] private float moveSpeed = 500;
	[SerializeField] private float moveSpeedMultiplier = 1;

	[Header("Fever")]
	[SerializeField] private float feverMultiplier = 1.5f;
	[SerializeField] private float feverDuration = 5;

	private Vector3 velocity;

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		if (!CanMove) return;
		velocity = Rb.velocity;
		velocity.z = moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime;
		if (IsFeverMode)
			velocity.z *= feverMultiplier;
		Rb.velocity = velocity;
	}

	public void FeverMode()
	{
		IsFeverMode = true;
		StopCoroutine(FeverModeOff());	//If enters fever mode again while in fever mode
		StartCoroutine(FeverModeOff());
	}

	private IEnumerator FeverModeOff()
	{
		yield return new WaitForSeconds(feverDuration);

		IsFeverMode = false;
	}
}