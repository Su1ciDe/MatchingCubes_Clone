using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeController : MonoBehaviour
{
	[SerializeField] private Cube cubePrefab;

	public List<Cube> Cubes { get; set; } = new List<Cube>();

	[Space]
	[SerializeField] private List<CubeColor> startingCubes = new List<CubeColor>();

	private Player player => Player.Instance;

	private readonly float cubeSize = 1f;

	public void AddCube(CollectableCube collectableCube, bool isAnimated = true)
	{
		var addedCube = Instantiate(cubePrefab, transform);
		addedCube.ChangeColor(collectableCube.CubeColor, collectableCube.Material);

		if (isAnimated)
			addedCube.transform.DOScale(0, .5f).From().SetEase(Ease.OutExpo);

		int count = Cubes.Count;
		for (int i = 0; i < count; i++)
		{
			var cube = Cubes[i];
			cube.transform.DOComplete();
			cube.transform.DOLocalJump(cube.transform.localPosition + cubeSize * Vector3.up, 1 + (count - i) / 2f, 1, .5f);
		}

		player.PlayerModel.DOComplete();
		player.PlayerModel.DOLocalJump(player.PlayerModel.localPosition + cubeSize * Vector3.up, 1 + count / 2f, 1, .5f);

		Cubes.Add(addedCube);

		ChangeTrail();

		CheckMatch();
	}

	public void RemoveCube(Cube cube = null)
	{
		player.SetActiveTrail(Cubes.Count > 0);

		ChangeTrail();

		CheckMatch();
	}

	public void CheckMatch()
	{
	}

	private void ChangeTrail()
	{
		if (Cubes.Count > 0)
		{
			player.SetActiveTrail(true);
			player.ChangeTrailColor(Cubes[^1].MaterialColor);
		}
		else
		{
			player.SetActiveTrail(false);
		}
	}
}