namespace Perfectris.Core.Types
{
	/// <summary>
	/// This allows me to use ref with delegates
	/// </summary>
	public class GameStateWrapper
	{
		private GameState _state;

		public GameStateWrapper(GameState state) => _state = state;
		public GameStateWrapper() => _state = new();

		public ref GameState Get() => ref _state;
	}
}