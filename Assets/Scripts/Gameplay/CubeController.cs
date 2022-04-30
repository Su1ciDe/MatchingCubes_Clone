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

	public readonly float CubeSize = 1f;

	public void AddCube(CollectableCube collectableCube, bool isAnimated = true)
	{
		var addedCube = Instantiate(cubePrefab, transform);
		addedCube.ChangeColor(collectableCube.CubeColor, collectableCube.Material);
		addedCube.IsInStack = true;

		if (isAnimated)
			addedCube.transform.DOScale(0, .5f).From().SetEase(Ease.OutBack);

		int count = Cubes.Count;
		for (int i = 0; i < count; i++)
		{
			var cube = Cubes[i];
			cube.transform.DOComplete();
			cube.transform.DOLocalJump(cube.transform.localPosition + CubeSize * Vector3.up, (count - i) / 2f, 1, .5f);
		}

		player.PlayerModel.DOComplete();
		player.PlayerModel.DOLocalJump(player.PlayerModel.localPosition + CubeSize * Vector3.up, count / 2f, 1, .5f);

		Cubes.Add(addedCube);

		ChangeTrail();

		CheckMatch();
	}

	public void RemoveCube(List<Cube> removedCubes)
	{
		foreach (Cube removedCube in removedCubes)
		{
			Cubes.Remove(removedCube);
			Destroy(removedCube.gameObject);
		}

		int count = Cubes.Count;
		for (int i = 0; i < count; i++)
		{
			Cube cube = Cubes[i];
			cube.transform.DOComplete();
			cube.transform.DOLocalMoveY((count - i - 1) * CubeSize, .5f).SetDelay((count - i)/ 10f).SetEase(Ease.InExpo);
		}

		player.PlayerModel.transform.DOComplete();
		player.PlayerModel.transform.DOLocalMoveY(count * CubeSize, .5f).SetDelay(count / 10f).SetEase(Ease.InExpo);

		player.SetActiveTrail(Cubes.Count > 0);
		ChangeTrail();

		CheckMatch();
	}

	public void CheckMatch()
	{
		int count = Cubes.Count;
		if (count < 3) return;
		for (int i = count - 1; i > 1; i--)
		{
			if (Cubes[i].CubeColor.Equals(Cubes[i - 1].CubeColor) && Cubes[i].CubeColor.Equals(Cubes[i - 2].CubeColor))
			{
				Matched(new List<Cube> { Cubes[i], Cubes[i - 1], Cubes[i - 2] });
				break;
			}
		}
	}

	private void Matched(List<Cube> cubes)
	{
		Sequence seq = DOTween.Sequence();
		foreach (Cube cube in cubes)
		{
			cube.IsInStack = false;
			cube.transform.DOComplete();
			seq.Join(cube.transform.DOPunchScale(1.2f * Vector3.one, .5f, 2, .5f));
		}
		seq.AppendCallback(() => RemoveCube(cubes));
	}

	public void ChangeTrail()
	{
		if (Cubes.Count > 0)
		{
			player.SetActiveTrail(true);
			player.ChangeTrailColor(Cubes[^1].Material.color);
		}
		else
		{
			player.SetActiveTrail(false);
		}
	}
}