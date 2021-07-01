using System;

namespace Perfectris
{
	public class SuperRotationSystem : IRotationSystem
	{
		public RotateResult Clockwise(Tetromino piece, GameState state) => throw new NotImplementedException();

		public RotateResult Anticlockwise(Tetromino piece, GameState state) => throw new NotImplementedException();

		public RotateResult Flip(Tetromino piece, GameState state) => throw new NotImplementedException();
	}
}