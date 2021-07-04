using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public class TopOutHandler
	{
		/// <summary>
		/// Only 1 bit of a piece not all of it has to lock down in the vanish zone
		/// </summary>
		public bool PartialLockOut;

		/// <summary>
		/// Check if block out will occur - top out due to a piece spawn being blocked
		/// </summary>
		public bool CheckBlockOut(bool[][] stack, Tetromino nextPiece)
		{
			var pieceInGrid = nextPiece.GetInGrid(stack[0].Length, stack.Length);
			return IntersectionChecker.CheckIntersect(pieceInGrid, stack);
		}

		/// <summary>
		/// Check if lock out will occur - top put due to a piece locking down fully / partially in the vanish zone
		/// </summary>
		public bool CheckLockOut(Tetromino nextPiece, int bufferZoneHeight)
			=> PartialLockOut
				   ? nextPiece.PosY                         <= bufferZoneHeight
				   : nextPiece.PosY + nextPiece.Grid.Length <= bufferZoneHeight;
	}
}