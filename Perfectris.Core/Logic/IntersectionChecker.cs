using System;

namespace Perfectris.Core.Logic
{
	public static class IntersectionChecker
	{
		public static bool WillHitLeftEdge(Tetromino piece, Func<bool[][], bool[][]> rotationFunc)
		{
			if (piece.PosX >= 0) return false; // piece cannot hit left edge unless negative PosX

			// How many columns are empty on the left before and after rotation
			var startOpenColumns = OpenColumnsFromLeft(piece.Grid);
			var endOpenColumns   = OpenColumnsFromLeft(rotationFunc(piece.Grid));

			// How much the piece moved to the left
			var pieceMovedLeft = endOpenColumns - startOpenColumns;

			// There must be more or equal space to the left than that we need to move in order to rotate
			return piece.PosX >= pieceMovedLeft;
		}
		
		public static bool WillHitRightEdge(Tetromino piece, int gridWidth, Func<bool[][], bool[][]> rotationFunc)
		{
			var maxPieceSize = Math.Max(piece.Grid.Length, piece.Grid[0].Length);
			var spaceToRight = gridWidth - piece.PosX - maxPieceSize;
			
			if (spaceToRight >= 0) return false; // piece cannot hit right edge in this case

			// How many columns are empty on the right before and after rotation
			var startOpenColumns = OpenColumnsFromRight(piece.Grid);
			var endOpenColumns   = OpenColumnsFromRight(rotationFunc(piece.Grid));

			// How much the piece moved to the right
			var pieceMovedRight = endOpenColumns - startOpenColumns;

			// There must be more or equal space to the right than that we need to move in order to rotate
			return spaceToRight >= pieceMovedRight;
		}

		private static int OpenColumnsFromLeft(bool[][] grid)
		{
			var openColumns = 0;

			for (var column = 0; column < grid[0].Length; column++)
			{
				var isOpen = true;
				foreach (var row in grid)
					if (row[column])
						isOpen = false;

				if (isOpen) openColumns++;
				else break;
			}

			return openColumns;
		}
		
		private static int OpenColumnsFromRight(bool[][] grid)
		{
			var openColumns = 0;

			for (var column = grid[0].Length - 1; column >= 0; column--)
			{
				var isOpen = true;
				foreach (var row in grid)
					if (row[column])
						isOpen = false;

				if (isOpen) openColumns++;
				else break;
			}

			return openColumns;
		}

		private static bool CheckIntersect(bool[][] paddedGrid, bool[][] stackGrid)
		{
			for (var y = 0; y < paddedGrid.Length; y++)
				for (var x = 0; x < stackGrid[y].Length; x++)
				{
					if (paddedGrid[y][x] && stackGrid[y][x])
						return true;
				}

			return false;
		}

		public static bool CheckValid(Tetromino                piece,      int offsetX, int offsetY, bool[][] stack,
									  Func<bool[][], bool[][]> rotateFunc, int gridWidth)
		{
			var gridX = stack[0].Length;
			var gridY = stack.Length;

			if (WillHitLeftEdge(piece, rotateFunc) || WillHitRightEdge(piece, gridWidth, rotateFunc))
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