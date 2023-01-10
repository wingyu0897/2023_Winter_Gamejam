using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSystem : MonoBehaviour, ICoreSystem
{
	private PlayerController player;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void UpdateState(GameState state)
	{
		switch (state)
		{
			case GameState.Init:
				player.Init();
				break;
			case GameState.Standby:
				player.Init();
				break;
			case GameState.Running:
				player.ActiveMove(true);
				break;
		}
	}
}
