using System;
using Perfectris.Core.Types;

namespace Perfectris.Core.Logic
{
	public static class IntersectionChecker
	{
		public static bool WillHitLeftEdge(Tetromino piece, Func<bool[][], bool[][]> rotationFunc)
			=> WillHitEdge(piece, rotationFunc, false);

		public static bool WillHitRightEdge(Tetromino piece, Func<bool[][], bool[][]> rotationFunc, int gridWidth)
			=> WillHitEdge(piece, rotationFunc, true, gridWidth);

		private static bool WillHitEdge(Tetromino piece, Func<bool[][], bool[][]> rotationFunc, bool rightEdge, int gridWidth = 0)
		{
			var maxPieceSize = Math.Max(piece.Grid.Length, piece.Grid[0].Length);
			var spaceToEdge  = rightEdge 
								   ? gridWidth - piece.PosX - maxPieceSize
								   : piece.PosX;
			
			if (spaceToEdge >= 0) return false; // must be clipping into the edge to collide with it
			
			// How many columns are empty on the edge of the piece grid before and after rotation
			var startOpenColumns = rightEdge
									   ? piece.OpenColumnsFromRight() 
									   : piece.OpenColumnsFromLeft();
			var endOpenColumns   = rightEdge 
									   ? rotationFunc(piece.Grid).OpenColumnsFromRight()
									   : rotationFunc(piece.Grid).OpenColumnsFromLeft();

			// How much the piece moved towards the edge
			var pieceMovedToEdge = endOpenColumns - startOpenColumns;

			// There must be more or equal space to the edge than the amount the piece moved towards it
			return spaceToEdge >= pieceMovedToEdge;
		}

		public static bool CheckIntersect(bool[][] grid1, bool[][] grid2)
		{
			for (var y = 0; y < Math.Min(grid1.Length, grid2.Length); y++)
				for (var x = 0; x < Math.Min(grid1[y].Length, grid2[y].Length); x++)
				{
					if (grid1[y][x] && grid2[y][x])
						return true;
				}

			return false;
		}

		public static bool CheckValid(Tetromino                piece,      int offsetX, int offsetY, bool[][] stack,
									  Func<bool[][], bool[][]> rotateFunc, int gridWidth)
		{
			var gridX = stack[0].Length;
			var gridY = stack.Length;

			if (WillHitLeftEdge(piece, rotateFunc) || WillHitRightEdge(piece, rotateFunc, gridWidth))
				return false;

			return !CheckIntersect(Tetromino.GetInGrid(rotateFunc(piece.Grid),
													   gridX,
													   gridY,
													   piece.PosX + offsetX,
													   piece.PosY + offsetY),
								   stack);
		}
	}
}