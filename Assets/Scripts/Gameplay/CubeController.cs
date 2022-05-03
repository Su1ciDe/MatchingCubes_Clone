using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CubeController : MonoBehaviour
{
	[SerializeField] private Cube cubePrefab;

	public List<Cube> Cubes { get; set; } = new List<Cube>();

	[Space]
	[SerializeField] private int matchCountForFever = 3;
	private int currentMatchCount;

	[Space]
	[SerializeField] private int matchReward = 1;


private Player player => Player.Instance;

	public readonly float CubeSize = 1f;
	private readonly float matchTimer = 1f;

	public static event UnityAction<Cube> OnCubeMatch; 

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
			var cube = Cubes[i].transform;
			cube.DOComplete();
			cube.DOLocalJump(cube.localPosition + CubeSize * Vector3.up, (count - i) / 2f, 1, .5f);
		}

		player.PlayerModel.DOComplete();
		player.PlayerModel.DOLocalJump(player.PlayerModel.localPosition + CubeSize * Vector3.up, count / 2f, 1, .5f);

		Cubes.Add(addedCube);

		ChangeTrail();

		CheckMatch();
	}

	public void RemoveCube(List<Cube> removedCubes, float destroyTime = 0)
	{
		foreach (Cube removedCube in removedCubes)
		{
			Cubes.Remove(removedCube);
			Destroy(removedCube.gameObject, destroyTime);
		}

		int count = Cubes.Count;
		for (int i = 0; i < count; i++)
		{
			Cube cube = Cubes[i];
			cube.transform.DOComplete();
			cube.transform.DOLocalMoveY((count - i - 1) * CubeSize, .5f).SetDelay((count - i) / 20f).SetEase(Ease.InExpo);
		}

		player.PlayerModel.transform.DOComplete();
		player.PlayerModel.transform.DOLocalMoveY(count * CubeSize, .5f).SetDelay(count / 20f).SetEase(Ease.InExpo);

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
		Player.Diamond += matchReward;
		
		currentMatchCount++;
		StopCoroutine(MatchIntervalWindow());
		StartCoroutine(MatchIntervalWindow());
		
		Sequence seq = DOTween.Sequence();
		foreach (Cube cube in cubes)
		{
			cube.IsInStack = false;
			cube.transform.DOComplete();
			seq.Join(cube.transform.DOPunchScale(1.2f * Vector3.one, .5f, 2, .5f));
		}

		seq.AppendCallback(() =>
		{
			OnCubeMatch?.Invoke(cubes[0]);
			
			RemoveCube(cubes);

			if (currentMatchCount >= matchCountForFever)
			{
				currentMatchCount = 0;
				player.PlayerMovement.FeverMode();
			}
		});
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

	private IEnumerator MatchIntervalWindow()
	{
		yield return new WaitForSeconds(matchTimer);

		currentMatchCount = 0;
	}
}