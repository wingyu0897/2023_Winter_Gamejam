public enum GameState
{
	Init,
	Standby,
	Running,
	Result
}

public interface ICoreSystem
{
	public void UpdateState(GameState state);
}
