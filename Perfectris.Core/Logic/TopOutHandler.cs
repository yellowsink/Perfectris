using System;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public class TopOutHandler
	{
		/// <summary>
		/// Only 1 bit of a piece not all of it has to lock down in the vanish zone
		/// </summary>
		public bool PartialLockOut;

		public bool CheckTopOut(TopOutCausingAction action, bool[][] stack, Tetromino nextPiece, int bufferZoneHeight = 0, int garbageRowsAdded = 0)
		{
			switch (action)
			{
				case TopOutCausingAction.Spawn:
					var pieceInGrid = nextPiece.GetInGrid(stack[0].Length, stack.Length);
					return IntersectionChecker.CheckIntersect(pieceInGrid, stack);
				
				case TopOutCausingAction.Lockdown:
					return PartialLockOut
							   ? nextPiece.PosY                         <= bufferZoneHeight
							   : nextPiece.PosY + nextPiece.Grid.Length <= bufferZoneHeight;
				
				case TopOutCausingAction.Garbage:
					// TODO: FIGURE OUT WHAT THE HELL "TOPOUT" ACTUALLY MEANS
					// My best guess for now is that garbage causes the currently dropping tetromino to be pushed into the buffer zone entirely
					var bottomRowOfPieceBefore = nextPiece.PosY         + nextPiece.Grid.Length;
					var bottomRowOfPieceAfter  = bottomRowOfPieceBefore + garbageRowsAdded;
					var inBufferBefore         = bottomRowOfPieceBefore <= bufferZoneHeight;
					var inBufferAfter          = bottomRowOfPieceAfter  <= bufferZoneHeight;
					
					return !inBufferBefore && inBufferAfter;
				
				default:
					throw new ArgumentOutOfRangeException(nameof(action), action, "Not a valid top out causing action");
			}
		}
	}

	public enum TopOutCausingAction
	{
		Spawn,
		Lockdown,
		Garbage
	}
}