using Perfectris.Core.Enums;
using Perfectris.Core.Logic.Rotation;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace Perfectris.Core.Logic
{
	public partial class TetrisLogic
	{
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

		/// <summary>
		/// How many ticks after lockdown or line clear to spawn another piece
		/// </summary>
		public int SpawnDelay;
	}
}