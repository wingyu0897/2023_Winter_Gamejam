using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour, ICoreSystem
{
    private float time = 0;
	private int score = 0;
	private bool isActive = false;

	private void Update()
	{
		if (isActive)
		{
			Scoring();
		}
	}

    private void Scoring()
	{
		time += Time.deltaTime;
		score = Mathf.FloorToInt(time);

		GameManager.Instance.score = score;
	}

	public void Init()
	{
		time = 0;
		score = 0;
		isActive = false;
	}

	public void UpdateState(GameState state)
	{
		switch (state)
		{
			case GameState.Init:
				Init();
				break;
			case GameState.Standby:
				Init();
				break;
			case GameState.Running:
				isActive = true;
				break;
			case GameState.Result:
				isActive = false;
				break;
		}
	}
}
