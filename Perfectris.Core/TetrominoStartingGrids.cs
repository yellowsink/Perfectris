using System.Collections.Generic;

namespace Perfectris.Core
{
	public static class TetrominoStartingGrids
	{
		public static readonly Dictionary<TetrominoType, bool[][]> StartingGrids = new()
		{
			{
				TetrominoType.I, new[]
				{
					new[] { false, false, false, false },
					new[] { true, true, true, true }
				}
			},
			{
				TetrominoType.J, new[]
				{
					new[] { true, false, false },
					new[] { true, true, true }
				}
			},
			{
				TetrominoType.L, new[]
				{
					new[] { false, false, true },
					new[] { true, true, true }
				}
			},
			{
				TetrominoType.O, new[]
				{
					new[] { false, true, true },
					new[] { false, true, true }
				}
			},
			{
				TetrominoType.S, new[]
				{
					new[] { false, true, true },
					new[] { true, true, false }
				}
			},
			{
				TetrominoType.T, new[]
				{
					new[] { false, true, false },
					new[] { true, true, true }
				}
			},
			{
				TetrominoType.Z, new[]
				{
					new[] { true, true, false },
					new[] { false, true, true }
				}
			}
		};
	}
}