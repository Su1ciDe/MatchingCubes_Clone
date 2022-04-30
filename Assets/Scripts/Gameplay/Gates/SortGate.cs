using UnityEngine;

public class SortGate : Gate
{
	public override void OnEnterGate(Player enteredPlayer)
	{
		var cubes = enteredPlayer.CubeController.Cubes;
		int count = cubes.Count;
		for (int i = 0; i < count - 1; i++)
		{
			for (int j = i + 1; j < count; j++)
			{
				if (cubes[i].CubeColor.Equals(cubes[j].CubeColor))
				{
					(cubes[i + 1].transform.localPosition, cubes[j].transform.localPosition) = (cubes[j].transform.localPosition, cubes[i + 1].transform.localPosition);
					(cubes[i + 1], cubes[j]) = (cubes[j], cubes[i + 1]);
					break;
				}
			}
		}

		enteredPlayer.CubeController.CheckMatch();
		enteredPlayer.CubeController.ChangeTrail();
	}
}