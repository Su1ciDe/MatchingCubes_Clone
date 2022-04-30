using UnityEngine;

public abstract class Gate : MonoBehaviour
{
	private bool isTriggered;

	public abstract void OnEnterGate(Player enteredPlayer);

	public void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			isTriggered = true;
			OnEnterGate(player);
		}
	}
}