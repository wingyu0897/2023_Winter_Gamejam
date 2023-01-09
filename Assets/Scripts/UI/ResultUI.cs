using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : UIState
{
	[SerializeField] private Button tapToStandby;

	private void Start()
	{
		Init();
	}

	public override void Init()
	{
		tapToStandby.onClick.AddListener(() =>
		GameManager.Instance.UpdateState
		(GameState.Standby));

		base.Init();
	}
}
