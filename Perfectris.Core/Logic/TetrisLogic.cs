using System;
using System.Linq;
using Perfectris.Core.Enums;
using Perfectris.Core.Logic.Rotation;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public partial class TetrisLogic
	{
		public TetrisLogic()
		{
			// These two values are from messing about in TETR.IO until it felt good :P
			SetDasRate(100, 33); // 33ms
			DasDelay = 150;      // 150ms

			SonicDrop = false;

			GravityBase     = 0.8m;
			GravityIncrease = 0.07m;
			SoftDropGravityFactor
				= 13; // TODO: I like 13x but this might be a bit high for default - will come back to this later
			SoftDropLock = false;

			RotationSystem = new SuperRotationSystem();

			LockdownTime = 50; // 50ms
			LockdownMode = LockdownMode.MoveReset;

			BufferZoneSize = 20;
			GridSizeX      = 10;
			GridSizeY      = 20;

			VisibleBuffer = false;

			LockDelay = 50; // 50ms
		}

		/// <summary>
		/// Runs every tick, runs checks, and if necessary performs actions, and re-renders the UI
		/// </summary>
		public void Update(GameLoop<GameStateWrapper> loop, Action<TetrominoType?[][]> setGrid)
		{
			if (CheckWait(loop, out var waitState))
			{
                RunWait(loop, waitState);
				return;
			}

			var spawn = CheckSpawn(loop);
			if (spawn)
			{
				RunSpawn(loop);
				Render();
				return;
			}
			
			var gravity  = CheckGravity(loop);
			var move     = CheckMove(loop);
			var softDrop = CheckGravity(loop, true);
			var lockDown = CheckLockdown(loop);
			
			if (gravity) RunGravity(loop);
			if (move) RunMove(loop);
			if (softDrop) RunGravity(loop, true);
			if (lockDown) RunLockdown(loop);
			
			if (gravity || move || softDrop || lockDown) Render();
			
			void Render()
			{
				var state = loop.State.Get();
				var pieceGrid = state.CurrentPiece?.GetInGrid(GridSizeX, GridSizeY)
									 .Select(row => row.Select(cell => cell 
																		   ? state.CurrentPiece.Type
																		   : (TetrominoType?) null)
													   .ToArray()).ToArray();

				setGrid(pieceGrid != null
							? state.Stack.CombineGrids(pieceGrid, (c1, c2) => c1 ?? c2)
							: state.Stack);
			}
		}
	}
}