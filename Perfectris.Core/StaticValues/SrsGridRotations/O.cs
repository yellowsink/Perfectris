using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> OClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, true },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, true, true },
					new[] { false, true, true }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> OAntiClockwise = OClockwise;
	}
}