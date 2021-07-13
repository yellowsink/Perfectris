using System;
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

		public void Update(GameLoop<GameStateWrapper> loop, Action<TetrominoType?[][]> setGrid)
		{
			var gravity  = CheckGravity(loop);
			var move     = CheckMove(loop);
			var softDrop = CheckGravity(loop, true);
			var lockDown = CheckLockdown(loop);

			if (gravity) RunGravity(loop);
			if (move) RunMove(loop);
			if (softDrop) RunGravity(loop, true);
			if (lockDown) RunLockdown(loop);

			if (gravity || move || softDrop || lockDown)
				setGrid(loop.State.Get().Stack);
		}
	}
}