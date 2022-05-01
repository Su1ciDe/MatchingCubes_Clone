using UnityEngine;

public class SpeedBooster : Booster
{
	public override void OnEnterBooster(Player enteredPlayer)
	{
		enteredPlayer.PlayerMovement.FeverMode();
	}
}