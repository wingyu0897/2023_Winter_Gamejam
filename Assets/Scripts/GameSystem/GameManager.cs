using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

	private List<ICoreSystem> coreSystems = new List<ICoreSystem>();
	private GameState currentState;

    public int score;
	public bool sideRighted = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		coreSystems.Add(new UISystem());

		coreSystems.Add(GetComponent<ScoreSystem>());
		coreSystems.Add(GetComponent<PlayerStateSystem>());
		coreSystems.Add(GetComponent<CameraSystem>());
		coreSystems.Add(GetComponent<LevelSystem>());

		UpdateState(GameState.Init);
	}

	public void UpdateState(GameState state)
	{
		for (int i = 0; i < coreSystems.Count; i++)
		{
			coreSystems[i].UpdateState(state);
		}

		switch (state)
		{
			case GameState.Init:
				UpdateState(GameState.Standby);
				break;
			default:
				break;
		}
	}
}
