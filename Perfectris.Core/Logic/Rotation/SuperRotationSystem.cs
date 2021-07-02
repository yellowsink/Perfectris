using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Perfectris.Core.StaticValues;

namespace Perfectris.Core.Logic.Rotation
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public class SuperRotationSystem : IRotationSystem
	{
		public RotateResult Clockwise(Tetromino piece, GameState state)
			=> RotateBase(piece, state, RotateGridCW, piece.Direction.Clockwise());

		public RotateResult Anticlockwise(Tetromino piece, GameState state)
			=> RotateBase(piece, state, RotateGridACW, piece.Direction.Anticlockwise());

		private static RotateResult RotateBase(Tetromino piece, GameState state, Func<bool[][], bool[][]> RotateFunc, Direction newDirection)
		{
			var stack = state.Stack.Select(row => row.Select(cell => cell.HasValue).ToArray()).ToArray();

			var testTable = piece.Type == TetrominoType.I ? SrsTestTables.I_Tests : SrsTestTables.JLSTZ_Tests;
			var tests     = testTable[(piece.Direction, newDirection)];
			foreach (var (x, y) in tests)
			{
				var valid = SrsIntersectionChecker.CheckValid(piece.Grid, piece.PosX + x, piece.PosY + y, stack);
				if (valid)
					return new RotateResult { NewGrid = RotateGridCW(piece.Grid), TranslateX = x, TranslateY = y };
			}

			return new RotateResult {NewGrid = piece.Grid};
		}

		private static bool[][] RotateGridCW(bool[][] grid)
			=> SrsGridRotator.Clockwise(grid) ?? throw new ArgumentOutOfRangeException(nameof(grid), "Not a valid tetromino grid");

		private static bool[][] RotateGridACW(bool[][] grid)
			=> SrsGridRotator.AntiClockwise(grid) ?? throw new ArgumentOutOfRangeException(nameof(grid), "Not a valid tetromino grid");
	}
}