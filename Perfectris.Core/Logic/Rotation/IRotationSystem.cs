namespace Perfectris.Core.Logic.Rotation
{
	public interface IRotationSystem
	{
		public RotateResult Clockwise(Tetromino     piece, GameState state);
		public RotateResult Anticlockwise(Tetromino piece, GameState state);
	}
}