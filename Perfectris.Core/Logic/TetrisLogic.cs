using System;
using Perfectris.Core.Enums;
using Perfectris.Core.Logic.Rotation;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public class TetrisLogic
	{
		// For more info on what DAS means visit https://tetris.wiki/DAS
		
		/// <summary>
		/// Delayed Auto Shift time in ticks - time between each move when holding a key
		/// </summary>
		public int DasTime;
		/// <summary>
		/// Sets the DAS time using the rate in Hz
		/// </summary>
		public void SetDasRate(decimal ticksPerSecond, decimal rate) => DasTime = (int) (ticksPerSecond / rate);
		/// <summary>
		/// Gets the DAS rate in Hz
		/// </summary>
		public decimal GetDasRate(decimal ticksPerSecond) => ticksPerSecond / DasTime;

		/// <summary>
		/// How many ticks after pressing a key Delayed Auto Shift activates
		/// </summary>
		public int DasDelay;

		/// <summary>
		/// Where gravity should start counting from
		/// </summary>
		public decimal GravityBase;
		/// <summary>
		/// How much to increase gravity each level - set to 0 to disable levelling
		/// </summary>
		public decimal GravityIncrease;
		
		/// <summary>
		/// The framerate to use for calculating gravity times
		/// </summary>
		public const int GravityRate = 60;

		/// <summary>
		/// What to multiply gravity by when soft dropping
		/// </summary>
		/// <returns></returns>
		public decimal SoftDropGravityFactor;
		/// <summary>
		/// Locks the piece instantly when soft dropping. See https://tetris.wiki/Drop#Soft_drop for helpful GIFs
		/// </summary>
		public bool SoftDropLock;
		
		/// <summary>
		/// If in doubt leave false - see https://tetris.wiki/Drop#Instant_dropping
		/// </summary>
		public bool SonicDrop;

		/// <summary>
		/// The rotation system to use - usually SRS
		/// </summary>
		public IRotationSystem RotationSystem;

		/// <summary>
		/// How many ticks it takes to lock a piece down
		/// </summary>
		public int LockdownTime;

		/// <summary>
		/// The Lockdown Mode - See https://tetris.wiki/Tetris_Guideline at "Lock Down"
		/// </summary>
		public LockdownMode LockdownMode;

		/// <summary>
		/// How large the buffer zone is
		/// </summary>
		public int BufferZoneSize;

		/// <summary>
		/// The amount of columns in the grid
		/// </summary>
		public int GridSizeX;
		/// <summary>
		/// The amount of rows in the grid
		/// </summary>
		public int GridSizeY;

		/// <summary>
		/// When false, a sliver of row 21 is shown. When true pieces in the buffer are fully visible above the grid
		/// </summary>
		public bool VisibleBuffer;

		/// <summary>
		/// How many ticks to lock down a piece
		/// </summary>
		public int LockDelay;

		public TetrisLogic()
		{
			// These two values are from messing about in TETR.IO until it felt good :P
			SetDasRate(100, 33); // 33ms
			DasDelay = 150; // 150ms
			
			SonicDrop       = false;

			GravityBase           = 0.8m;
			GravityIncrease       = 0.07m;
			SoftDropGravityFactor = 13; // TODO: I like 13x but this might be a bit high for default - will come back to this later
			SoftDropLock          = false;

			RotationSystem = new SuperRotationSystem();

			LockdownTime = 50; // 50ms
			LockdownMode = LockdownMode.MoveReset;
			
			BufferZoneSize = 20;
			GridSizeX      = 10;
			GridSizeY      = 20;

			VisibleBuffer = false;

			LockDelay = 50; // 50ms
		}

		public void Update(GameLoop<GameStateWrapper> loop, Action<TetrominoType?[][]> setGrid)
		{
			var gravityUpdate = CheckGravity(loop);
			if (gravityUpdate) RunGravity(loop);
			var moveUpdate = CheckMove(loop);
			if (moveUpdate) RunMove(loop);
			var softDropUpdate = CheckGravity(loop, true);
			if (softDropUpdate) RunGravity(loop, true);
			
			
			if (gravityUpdate || moveUpdate)
				setGrid(loop.State.Get().Stack);
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
		/// Applies movement for DAS or standard movement
		/// </summary>
		private void RunMove(GameLoop<GameStateWrapper> loop)
		{
			ref var stateRef   = ref loop.State.Get();

			switch (stateRef.InputState.MoveDirection)
			{
				case MoveDirection.None:
					break;
				case MoveDirection.Left:
					if (stateRef.CurrentPiece is { PosX: > 0 })
						stateRef.CurrentPiece.PosX--;
					break;
				case MoveDirection.Right:
					if (stateRef.CurrentPiece is not { })
						break;

					var maxRight = GridSizeX + stateRef.CurrentPiece.OpenColumnsFromRight();
					
					if (stateRef.CurrentPiece.PosX < maxRight - 1)
						stateRef.CurrentPiece.PosX++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		

		/// <summary>
		/// Checks if gravity should be applied this tick
		/// </summary>
		private bool CheckGravity(GameLoop<GameStateWrapper> loop, bool softDrop = false)
		{
			ref var stateRef      = ref loop.State.Get();

			var ticksNoOffset = loop.CurrentTick - stateRef.GravityTickOffset;

			var (ticksPerMove, _) = GravityToTicks(GetGravity(stateRef.Level, softDrop));
			return ticksNoOffset % ticksPerMove == 0; // See the return statement on CheckMove for why this works
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

			if (stateRef.CurrentPiece != null) stateRef.CurrentPiece.PosY += cellsToMove;
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
	}

	public enum LockdownMode
	{
		Infinity,
		MoveReset,
		StepReset
	}
}