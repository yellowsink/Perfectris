using System;
using System.Linq;
using Perfectris.Core.Enums;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public partial class TetrisLogic
	{
		/// <summary>
		/// Applies movement for DAS or standard movement
		/// </summary>
		private void RunMove(GameLoop<GameStateWrapper> loop)
		{
			ref var stateRef = ref loop.State.Get();

			switch (stateRef.InputState.MoveDirection)
			{
				case MoveDirection.None:
					break;
				case MoveDirection.Left:
					if (stateRef.CurrentPiece.PosX > 0)
						stateRef.CurrentPiece.PosX--;
					break;
				case MoveDirection.Right:
					var maxRight = GridSizeX + stateRef.CurrentPiece.OpenColumnsFromRight();

					if (stateRef.CurrentPiece.PosX < maxRight - 1)
						stateRef.CurrentPiece.PosX++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}


		/// <summary>
		/// Applies gravity once
		/// </summary>
		private void RunGravity(GameLoop<GameStateWrapper> loop, bool softDrop = false)
		{
			ref var stateRef = ref loop.State.Get();

			if (stateRef.GravityTickTimer > 0) // Deal with what to do if gravity is paused
			{
				stateRef.GravityTickTimer--;
				stateRef.GravityTickOffset++;
				return;
			}

			var (_, cellsToMove) = GravityToTicks(GetGravity(stateRef.Level, softDrop));

			stateRef.CurrentPiece.PosY += cellsToMove;
		}

		/// <summary>
		/// Gets the gravity for the given level
		/// </summary>
		private decimal GetGravity(int level, bool softDrop = false)
			=> (GravityBase + GravityIncrease * (level - 1)) * (softDrop ? SoftDropGravityFactor : 1);

		/// <summary>
		/// Calculates the ticks per move and cells to move from a gravity value
		/// </summary>
		private static (int, int) GravityToTicks(decimal gravity)
		{
			var framesPerCell = 1m / gravity;

			var cellsPerMove = 1;
			var ticksPerMove = 0m;

			while (ticksPerMove < 1) // less than 1 tick per cell
			{
				cellsPerMove++;
				ticksPerMove = framesPerCell * GravityRate / 100 * cellsPerMove;
			}

			return ((int) ticksPerMove, cellsPerMove);
		}

		/// <summary>
		/// Locks the current piece down
		/// </summary>
		private void RunLockdown(GameLoop<GameStateWrapper> loop)
		{
			ref var stateRef = ref loop.State.Get();

			var pieceType = stateRef.CurrentPiece.Type;
			var pieceInGrid = stateRef.CurrentPiece.GetInGrid(GridSizeX, GridSizeY)
									  .Select(row => row.Select(cell => cell ? pieceType : (TetrominoType?) null)
														.ToArray()).ToArray();

			var stack    = stateRef.Stack;
			var combined = stack.CombineGrids(pieceInGrid, (cell1, cell2) => cell1 ?? cell2);

			stateRef.Stack = combined;
		}
	}
}