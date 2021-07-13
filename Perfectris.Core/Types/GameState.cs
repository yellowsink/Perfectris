#nullable enable
using Perfectris.Core.Enums;

namespace Perfectris.Core.Types
{
	public class GameState
	{
		/// <summary>
		/// How many ticks offset from 0 before the DAS cycle begins
		/// </summary>
		public int DasTickOffset = 0;

		/// <summary>
		/// If something temporarily pauses gravity, this will be incremented that many ticks
		/// </summary>
		public int GravityTickOffset = 0;

		/// <summary>
		/// Counts down to add to offset when you want to pause - here to make pausing gravity much easier
		/// </summary>
		public int GravityTickTimer = 0;

		/// <summary>
		/// How many ticks left until the piece locks down
		/// </summary>
		public int LockDownTimer = 0;

		/// <summary>
		/// Has the player used Hold this turn?
		/// </summary>
		public bool HasHeldThisTurn = false;
		/// <summary>
		/// What has the player got held?
		/// </summary>
		public TetrominoType? HeldPiece = null;

		/// <summary>
		/// The stack (and buffer). Tetromino type is used to determine the right colour
		/// </summary>
		public TetrominoType?[][] Stack = { new TetrominoType?[] { null } };

		/// <summary>
		/// State of inputs
		/// </summary>
		public InputState InputState = new();

		/// <summary>
		/// The player's current level
		/// </summary>
		public int Level = 1;

		public Tetromino CurrentPiece = null!;
	}
}