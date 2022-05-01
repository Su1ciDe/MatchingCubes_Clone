using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LavaObstacle : Obstacle
{
	public override void OnEnterObstacle(Cube triggeredCube)
	{
		triggeredCube.transform.DOScale(0, .5f).SetEase(Ease.OutExpo);
		Player.Instance.CubeController.RemoveCube(new List<Cube> { triggeredCube });
	}

	public override void OnEnterObstacleWhileFeverMode(Cube triggeredCube)
	{
	}
}