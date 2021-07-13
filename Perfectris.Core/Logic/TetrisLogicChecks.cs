using System.Linq;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public partial class TetrisLogic
	{
		/// <summary>
		/// Checks if we need to wait for something
		/// </summary>
		private static bool CheckWait(GameLoop<GameStateWrapper> loop, out WaitingState waitingState)
		{
			ref var stateRef = ref loop.State.Get();
			waitingState = stateRef.WaitingState;
			
			return stateRef.Timers.SpawnWait != 0;
		}
		
		/// <summary>
		/// Checks if a piece should be moved via DAS or manually this tick
		/// </summary>
		private bool CheckMove(GameLoop<GameStateWrapper> loop)
		{
			if (loop.CurrentTick == loop.State.Get().InputState.PressDownTick)
				return true;

			var tickOnCycle    = loop.CurrentTick - loop.State.Get().DasTickOffset;
			var postDelayTicks = tickOnCycle      - DasDelay;
			if (postDelayTicks < 0) return false; // DAS has not activated yet

			// If the post-delay ticks is a multiple of DelayTime then the modulo = 0 - this makes it repeat correctly
			return postDelayTicks % DasTime == 0;
		}

		/// <summary>
		/// Checks if gravity should be applied this tick
		/// </summary>
		private bool CheckGravity(GameLoop<GameStateWrapper> loop, bool softDrop = false)
		{
			ref var stateRef = ref loop.State.Get();

			var ticksNoOffset = loop.CurrentTick - stateRef.GravityPauseOffset;

			var (ticksPerMove, _) = GravityToTicks(GetGravity(stateRef.Level, softDrop));
			
			return ticksNoOffset % ticksPerMove == 0; // See the return statement on CheckMove for why this works
		}

		/// <summary>
		/// Checks if a piece should be locked down this tick
		/// </summary>
		private bool CheckLockdown(GameLoop<GameStateWrapper> loop)
		{
			ref var stateRef = ref loop.State.Get();

			if (stateRef.CurrentPiece == null)
				return false;
			
			var maxDown = GridSizeX - stateRef.CurrentPiece.Grid.Length +
						  stateRef.CurrentPiece.OpenRowsFromBottom();

			var isTouchingFloor = maxDown == 0;

			if (!isTouchingFloor)
			{
				var stack = stateRef.Stack.Select(row => row.Select(cell => cell != null).ToArray()).ToArray();

				var piece = stateRef.CurrentPiece;
				piece.PosY++;
				var pieceGrid = piece.GetInGrid(GridSizeX, GridSizeY);

				isTouchingFloor = IntersectionChecker.CheckIntersect(pieceGrid, stack);
			}

			if (!isTouchingFloor) return false;

			if (stateRef.Timers.LockDown == 0)
			{
				stateRef.Timers.LockDown  = LockdownTime;
				stateRef.Timers.SpawnWait = SpawnDelay;
				stateRef.WaitingState     = WaitingState.WaitingForSpawn;
				return true;
			}

			stateRef.Timers.LockDown--;
			return false;
		}

		/// <summary>
		/// Checks if we should spawn a piece
		/// </summary>
		private bool CheckSpawn(GameLoop<GameStateWrapper> loop)
		{
			ref var stateRef = ref loop.State.Get();
			return stateRef.WaitingState == WaitingState.WaitingForSpawn && stateRef.Timers.SpawnWait == 0 ;
		}
	}
}