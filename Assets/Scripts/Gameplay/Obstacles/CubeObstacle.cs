using System.Collections.Generic;
using UnityEngine;

public class CubeObstacle : Obstacle
{
	public override void OnEnterObstacle(Cube triggeredCube)
	{
		triggeredCube.transform.SetParent(null);
		triggeredCube.gameObject.AddComponent<Rigidbody>();
		Player.Instance.CubeController.RemoveCube(new List<Cube> { triggeredCube }, 5);
	}

	public override void OnEnterObstacleWhileFeverMode(Cube triggeredCube)
	{
		var colliders = Physics.OverlapSphere(triggeredCube.transform.position, 10, LayerMask.GetMask("Obstacle"));
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].attachedRigidbody && colliders[i].attachedRigidbody.TryGetComponent(out CubeObstacle cubeObstacle))
			{
				colliders[i].attachedRigidbody.isKinematic = false;
				colliders[i].attachedRigidbody.AddExplosionForce(1000, triggeredCube.transform.position, 10);
				Destroy(colliders[i].gameObject, 4);
			}
		}
	}
}