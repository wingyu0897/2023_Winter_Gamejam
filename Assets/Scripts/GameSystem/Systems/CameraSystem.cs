using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour, ICoreSystem
{
	private CameraMovement camMove;

	private void Awake()
	{
		camMove = Camera.main.GetComponent<CameraMovement>();
	}

	public void UpdateState(GameState state)
	{
		switch (state)
		{
			case GameState.Init:
				camMove.isActive = false;
				break;
			case GameState.Running:
				camMove.isActive = true;
				break;
			case GameState.Result:
				camMove.isActive = false;
				break;
		}
	}
}
