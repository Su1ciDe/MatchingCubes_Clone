using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
	private bool isTriggered;

	public void OnTriggerEnter(Collider other)
	{
		if (!isTriggered && other.transform.parent && other.transform.parent.TryGetComponent(out Cube cube) && cube.IsInStack)
		{
			isTriggered = true;
			if (!Player.Instance.PlayerMovement.IsFeverMode)
			{
				if (Player.Instance.CubeController.Cubes.Count > 0)
					OnEnterObstacle(cube);
				else
					LevelManager.Instance.GameFail();
			}
			else
				OnEnterObstacleWhileFeverMode(cube);
		}
	}

	public abstract void OnEnterObstacle(Cube triggeredCube);
	public abstract void OnEnterObstacleWhileFeverMode(Cube triggeredCube);
}