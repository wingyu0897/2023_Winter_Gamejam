using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : ICoreSystem
{
	private List<UIState> uiStates = new List<UIState>();

	public UISystem()
	{
		uiStates.Add(GameObject.Find("InitUI").GetComponent<UIState>());
		uiStates.Add(GameObject.Find("StandbyUI").GetComponent<UIState>());
		uiStates.Add(GameObject.Find("RunningUI").GetComponent<UIState>());
		uiStates.Add(GameObject.Find("ResultUI").GetComponent<UIState>());
	}

	public void UpdateState(GameState state)
	{
		switch (state)
		{
			case GameState.Init:
				InitAllUI();
				CloseAllUI();
				break;
			default:
				ActiveScreen(state);
				break;
		}
	}

	private void ActiveScreen(GameState state)
	{
		CloseAllUI();

		for (int i = 0; i < uiStates.Count; i++)
		{
			if (uiStates[i].state == state)
				uiStates[i].UpdateScreenState(true);
		}
	}

	private void CloseAllUI()
	{
		foreach (UIState ui in uiStates)
		{
			ui.UpdateScreenState(false);
		}
	}

	private void InitAllUI()
	{
		foreach (var ui in uiStates)
		{
			ui.Init();
		}
	}
}
