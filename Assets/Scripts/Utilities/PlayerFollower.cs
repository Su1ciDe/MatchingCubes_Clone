using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
	private void FixedUpdate()
	{
		transform.position = new Vector3(0, Player.Instance.PlayerModel.transform.position.y, Player.Instance.PlayerModel.transform.position.z);
	}
}