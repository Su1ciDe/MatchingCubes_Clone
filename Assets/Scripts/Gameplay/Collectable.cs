using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
	public abstract void OnCollect(Player collectorPlayer);

	public void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			OnCollect(player);
		}
	}
}