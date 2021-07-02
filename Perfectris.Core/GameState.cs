namespace Perfectris.Core
{
	public class GameState
	{
		/// <summary>
		/// How many ticks offset from 0 before the DAS cycle begins
		/// </summary>
		public int DasTickOffset;

		/// <summary>
		/// How many ticks left until the piece locks down
		/// </summary>
		public int LockDownTimer;

		/// <summary>
		/// Has the player used Hold this turn?
		/// </summary>
		public bool HasHeldThisTurn;
		/// <summary>
		/// What has the player got held?
		/// </summary>
		public TetrominoType? HeldPiece;
		/// <summary>
		/// Convenience value for if a piece is held
		/// </summary>
		public bool IsPieceHeld => HeldPiece.HasValue;

		/// <summary>
		/// The stack. Tetromino type is used to determine the right colour
		/// </summary>
		public TetrominoType?[][] Stack = { new TetrominoType?[] { null } };

		/// <summary>
		/// State of inputs
		/// </summary>
		public InputState InputState = new();
	}
}