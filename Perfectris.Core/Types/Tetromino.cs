using System.Linq;
using Perfectris.Core.Enums;
using Perfectris.Core.Logic;
using Perfectris.Core.StaticValues;

namespace Perfectris.Core.Types
{
	public class Tetromino
	{
		public bool[][]      Grid;
		public Direction     Direction;
		public int           PosX;
		public int           PosY;
		public TetrominoType Type;

		public Tetromino(TetrominoType type, int gridSizeX, int gridSizeY)
		{
			Type         = type;
			Grid         = TetrominoStartingGrids.StartingGrids[type];
			(PosX, PosY) = type.GetSpawnPos(gridSizeX, gridSizeY);
		}

		
		public bool[][] GetInGrid(int gridSizeX, int gridSizeY) => GetInGrid(Grid, gridSizeX, gridSizeY, PosX, PosY);

		public static bool[][] GetInGrid(bool[][] piece, int gridSizeX, int gridSizeY, int posX, int posY)
			=> piece.Select(row => row.Pad(gridSizeX, false, posX)).ToArray()
					.Pad(gridSizeY, Enumerable.Repeat(false, gridSizeX).ToArray(), posY);
	}
}