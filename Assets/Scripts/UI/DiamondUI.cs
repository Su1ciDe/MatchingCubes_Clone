using DG.Tweening;
using TMPro;
using UnityEngine;

public class DiamondUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtDiamondCount;
	[SerializeField] private Transform imageTarget;

	[SerializeField] private float diamondAnimDuration = .5f;

	private void Start()
	{
		txtDiamondCount.SetText(Player.Diamond.ToString());
	}

	private void OnEnable()
	{
		CubeController.OnCubeMatch += OnMoneyChanged;
	}

	private void OnDisable()
	{
		CubeController.OnCubeMatch -= OnMoneyChanged;
	}

	private void OnMoneyChanged(Cube cube)
	{
		Sequence seq = DOTween.Sequence();

		var imgCoin = ObjectPooler.Instance.Spawn("Diamond", GameManager.MainCamera.WorldToScreenPoint(cube.transform.position), transform);
		seq.Append(imgCoin.transform.DOMove(imageTarget.position, diamondAnimDuration).SetEase(Ease.InBack));
		seq.Append(imageTarget.DOPunchScale(Vector3.one * .9f, .2f, 2, .5f));
		seq.AppendCallback(() =>
		{
			imgCoin.SetActive(false);
			txtDiamondCount.SetText(Player.Diamond.ToString());
		});
	}
}