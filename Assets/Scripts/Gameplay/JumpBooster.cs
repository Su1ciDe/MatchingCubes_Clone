using DG.Tweening;
using UnityEngine;

public class JumpBooster : Booster
{
	[SerializeField] private Transform jumpPoint;
	[SerializeField] private float jumpHeight = 1;
	[SerializeField] private float jumpSpeed = 1;
	[SerializeField] private AnimationCurve jumpCurve;

	public override void OnEnterBooster(Player enteredPlayer)
	{
		enteredPlayer.PlayerController.CanPlay = false;
		enteredPlayer.PlayerMovement.CanMove = false;
		enteredPlayer.Trail.emitting = false;

		enteredPlayer.transform.DOJump(jumpPoint.position, jumpHeight, 1, jumpSpeed).SetSpeedBased(true).SetUpdate(UpdateType.Fixed).SetEase(jumpCurve).OnComplete(() =>
		{
			enteredPlayer.PlayerController.CanPlay = true;
			enteredPlayer.PlayerMovement.CanMove = true;
			enteredPlayer.Trail.emitting = true;
		});

		int count = enteredPlayer.CubeController.Cubes.Count;
		for (int i = 0; i < count; i++)
		{
			var cube = enteredPlayer.CubeController.Cubes[i];
			cube.transform.DOComplete();
			cube.transform.DOLocalJump(cube.transform.localPosition, (count - i) / 2f, 1, jumpSpeed).SetSpeedBased(true);
		}

		enteredPlayer.PlayerModel.DOComplete();
		enteredPlayer.PlayerModel.DOLocalJump(enteredPlayer.PlayerModel.localPosition, count / 2f, 1, jumpSpeed).SetSpeedBased(true);
	}
}