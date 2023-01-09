using UnityEngine;
using UnityEngine.UI;

public class StandbyUI : UIState
{
	[SerializeField] private Button tapToRunning;

	private void Start()
	{
		Init();
	}

	public override void Init()
	{
		tapToRunning.onClick.AddListener(() =>
		GameManager.Instance.UpdateState
		(GameState.Running));

		base.Init();
	}
}
