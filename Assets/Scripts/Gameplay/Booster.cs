using UnityEngine;

public abstract class Booster : MonoBehaviour
{
	private bool isTriggered;

	public abstract void OnEnterBooster(Player enteredPlayer);

	public void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			isTriggered = true;
			OnEnterBooster(player);
		}
	}
}
