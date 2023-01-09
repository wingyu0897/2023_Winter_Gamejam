using UnityEngine;
using UnityEngine.UI;

public class StandbyButton : UIState
{
	[SerializeField] private Button tapToStandby;

	private void Awake()
	{
		Init();
	}

	public override void Init()
	{
		tapToStandby.onClick.AddListener(() =>
		GameManager.Instance.UpdateState
		(GameState.Running));

		base.Init();
	}
}
