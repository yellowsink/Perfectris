using System.Linq;

namespace Perfectris.Core.Logic.Rotation
{
	public static class SrsIntersectionChecker
	{
		private static bool CheckIntersect(bool[][] paddedGrid, bool[][] stackGrid)
		{
			// Add border so it intersects with the edge of the grid
			var borderedPieceGrid = AddBorders(paddedGrid, false);
			var borderedStackGrid = AddBorders(stackGrid,  true);

			for (var y = 0; y < borderedPieceGrid.Length; y++)
				for (var x = 0; x < borderedStackGrid[y].Length; x++)
				{
					if (borderedPieceGrid[y][x] && borderedStackGrid[y][x])
						return true;
				}

			return false;
		}

		private static bool[][] AddBorders(bool[][] original, bool value)
		{
			var sideBordered = original.Select(row => new[] { value }.Concat(row).Append(value).ToArray())
									   .ToArray();

			var emptyRow = Enumerable.Repeat(value, sideBordered[0].Length).ToArray();
			var bordered = new[] { emptyRow }.Concat(sideBordered).Append(emptyRow).ToArray();

			return bordered;
		}

		internal static bool CheckValid(bool[][] piece, int posX, int posY, bool[][] stack)
		{
			var gridX = stack[0].Length;
			var gridY = stack.Length;

			var intersects = CheckIntersect(Tetromino.GetInGrid(piece, gridX, gridY, posX, posY), stack);
			return !intersects;
		}
	}
}