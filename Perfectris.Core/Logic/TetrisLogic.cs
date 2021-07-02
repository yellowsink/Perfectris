using System;
using Perfectris.Core.Logic.Rotation;

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
		/// Where to start counting gravity from
		/// </summary>
		public decimal GravityOffset;
		/// <summary>
		/// Calculator for how much to increase gravity for a level
		/// </summary>
		public Func<decimal, int, decimal> GravityIncrease;

		/// <summary>
		/// The framerate to use for calculating gravity times
		/// </summary>
		public const int GravityRate = 60;

		/// <summary>
		/// The gravity to use for soft drop. Will never soft drop slower than normal gravity
		/// </summary>
		/// <returns></returns>
		public decimal SoftDropGravity;
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

		public TetrisLogic()
		{
			SetDasRate(100, 30);
			DasDelay = 10;
			
			SonicDrop       = false;
			
			GravityOffset   = 1m / 64;
			GravityIncrease = (gravity, level) => gravity - (1 / ((1 / gravity) * (decimal) Math.Pow(2, level)));
			SoftDropGravity = 2m / 3; // 40 / 60 - 40 cells per second / GravityRate
			SoftDropLock    = false;

			RotationSystem = new SuperRotationSystem();

			LockdownTime = 50; // 0.5 seconds * 100 ticks per second
			LockdownMode = LockdownMode.MoveReset;
		}
		
		public bool IsRenderNecessary(GameLoop<GameState> l)
		{
			return false;
		}

		public void Render(GameLoop<GameState> loop, Action<TetrominoType?[][]> setGrid)
		{
		}
	}

	public enum LockdownMode
	{
		Infinity,
		MoveReset,
		StepReset
	}
}