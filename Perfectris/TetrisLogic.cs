using System;
using Avalonia.Media;

namespace Perfectris
{
	public class TetrisLogic
	{
		// For more info on what DAS means visit https://tetris.wiki/DAS
		
		/// <summary>
		/// Delayed Auto Shift time in seconds - time between each move when holding a key
		/// </summary>
		public decimal DasTime;
		/// <summary>
		/// convenient interface to access DasTime but in Hz
		/// </summary>
		public decimal DasRate
		{
			get => 1 / DasTime;
			set => DasTime = 1 / value;
		}
		/// <summary>
		/// How long after pressing a key Delayed Auto Shift activates
		/// </summary>
		public decimal DasDelay;

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

		public TetrisLogic()
		{
			DasRate         = 30;
			DasDelay        = 10;
			
			SonicDrop       = false;
			
			GravityOffset   = (decimal) 0.015625; // 1/64
			GravityIncrease = (gravity, level) => gravity - (1 / ((1 / gravity) * (decimal) Math.Pow(2, level)));
			SoftDropGravity = (decimal) (2.0 / 3); // 40 / 60 - 40 cells per second / GravityRate
			SoftDropLock    = false;
		}
		
		public bool IsRenderNecessary(GameLoop<GameState> loop)
		{
			return false;
		}

		public void Render(GameLoop<GameState> loop, Action<Color[][]> setGrid)
		{
			
		}
	}
}