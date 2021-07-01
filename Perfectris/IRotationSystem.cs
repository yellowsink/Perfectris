namespace Perfectris
{
	public interface IRotationSystem
	{
		public RotateResult Clockwise(Tetromino     piece, GameState state);
		public RotateResult Anticlockwise(Tetromino piece, GameState state);
		public RotateResult Flip(Tetromino          piece, GameState state);
	}
}