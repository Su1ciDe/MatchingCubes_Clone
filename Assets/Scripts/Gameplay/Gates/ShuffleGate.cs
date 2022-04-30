using UnityEngine;

public class ShuffleGate : Gate
{
	public override void OnEnterGate(Player enteredPlayer)
	{
		var cubes = enteredPlayer.CubeController.Cubes;
		int count = cubes.Count;
		var rng = new System.Random(Random.Range(0, 1000));
		while (count > 1)
		{
			count--;
			int k = rng.Next(count + 1);
			(cubes[k].transform.localPosition, cubes[count].transform.localPosition) = (cubes[count].transform.localPosition, cubes[k].transform.localPosition);
			(cubes[k], cubes[count]) = (cubes[count], cubes[k]);
		}

		enteredPlayer.CubeController.ChangeTrail();
	}
}