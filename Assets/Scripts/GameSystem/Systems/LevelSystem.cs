using UnityEngine;

public class LevelSystem : MonoBehaviour, ICoreSystem
{
	private LevelManager levelManager;

	private void Awake()
	{
		levelManager = GetComponentInChildren<LevelManager>();
	}

	public void UpdateState(GameState state)
	{
		switch (state)
		{
			case GameState.Init:
				levelManager.Initialize();
				break;
			case GameState.Standby:
				levelManager.Initialize();
				break;
			case GameState.Running:
				levelManager.Active();
				break;
		}
	}
}
