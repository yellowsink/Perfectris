using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Perfectris.Core.Enums;

namespace Perfectris.Core.StaticValues
{
	// For a given rotation, test intersection by rotating then applying each translation. Only fail to rotate if all 5 tests fail.
	// These tables are taken from https://tetris.wiki/Super_Rotation_System
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public static class SrsTestTables
	{
		public static readonly Dictionary<(Direction, Direction), (int, int)[]> JLSTZ_Tests = new()
		{
			{ (Direction.Up, Direction.Right), new[] { (0, 0), (-1, 0), (-1, 1), (0, -2), (-1, -2) } },
			{ (Direction.Right, Direction.Up), new[] { (0, 0), (1, 0), (1, -1), (0, 2), (1, 2) } },
			{ (Direction.Right, Direction.Down), new[] { (0, 0), (1, 0), (1, -1), (0, 2), (1, 2) } },
			{ (Direction.Down, Direction.Right), new[] { (0, 0), (-1, 0), (-1, 1), (0, -2), (-1, -2) } },
			{ (Direction.Down, Direction.Left), new[] { (0, 0), (1, 0), (1, 1), (0, -2), (1, -2) } },
			{ (Direction.Left, Direction.Down), new[] { (0, 0), (-1, 0), (-1, -1), (0, 2), (-1, 2) } },
			{ (Direction.Left, Direction.Up), new[] { (0, 0), (-1, 0), (-1, -1), (0, 2), (-1, 2) } },
			{ (Direction.Up, Direction.Left), new[] { (0, 0), (1, 0), (1, 1), (0, -2), (1, -2) } }
		};

		public static readonly Dictionary<(Direction, Direction), (int, int)[]> I_Tests = new()
		{
			{ (Direction.Up, Direction.Right), new[] { (0, 0), (-2, 0), (1, 0), (-2, -1), (1, 2) } },
			{ (Direction.Right, Direction.Up), new[] { (0, 0), (2, 0), (-1, 0), (2, 1), (-1, -2) } },
			{ (Direction.Right, Direction.Down), new[] { (0, 0), (-1, 0), (2, 0), (-1, 2), (2, -1) } },
			{ (Direction.Down, Direction.Right), new[] { (0, 0), (1, 0), (-2, 0), (1, -2), (-2, 1) } },
			{ (Direction.Down, Direction.Left), new[] { (0, 0), (2, 0), (-1, 0), (2, 1), (-1, -2) } },
			{ (Direction.Left, Direction.Down), new[] { (0, 0), (-2, 0), (1, 0), (-2, -1), (1, 2) } },
			{ (Direction.Left, Direction.Up), new[] { (0, 0), (1, 0), (-2, 0), (1, -2), (-2, 1) } },
			{ (Direction.Up, Direction.Left), new[] { (0, 0), (-1, 0), (2, 0), (-1, 2), (2, -1) } }
		};
	}
}