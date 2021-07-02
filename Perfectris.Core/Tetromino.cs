using System.Linq;
using Perfectris.Core.Logic;

namespace Perfectris.Core
{
	public class Tetromino
	{
		public bool[][]      Grid;
		public Direction     Direction;
		public int           PosX;
		public int           PosY;
		public TetrominoType Type;

		public Tetromino(TetrominoType type)
		{
			Type = type;
			Grid = TetrominoStartingGrids.StartingGrids[type];
		}

		
		public bool[][] GetInGrid(int gridSizeX, int gridSizeY) => GetInGrid(Grid, gridSizeX, gridSizeY, PosX, PosY);

		public static bool[][] GetInGrid(bool[][] piece, int gridSizeX, int gridSizeY, int posX, int posY)
			=> piece.Select(row => row.Pad(gridSizeX, false, posX)).ToArray()
					.Pad(gridSizeY, Enumerable.Repeat(false, gridSizeX).ToArray(), posY);
	}
}