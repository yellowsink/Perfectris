#nullable enable
using System.Collections.Generic;
using Perfectris.Core.StaticValues;

namespace Perfectris.Core.Logic.Rotation
{
	// This isn't a particularly clean way to do it, but the code to do it was hard so here we are.
	public static class SrsGridRotator
	{
		public static bool[][]? Clockwise(bool[][]     grid) => SrsGridRotations.Clockwise.GetValueOrDefault(grid);
		public static bool[][]? AntiClockwise(bool[][] grid) => SrsGridRotations.Anticlockwise.GetValueOrDefault(grid);
	}
}