using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> LClockwise = new()
		{
			{
				new[]
				{
					new[] { false, false, true },
					new[] { true, true, true }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, false },
					new[] { false, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, false },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, true },
					new[] { true, false, false }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, true },
					new[] { true, false, false }
				},
				new[]
				{
					new[] { true, true },
					new[] { false, true },
					new[] { false, true }
				}
			},

			{
				new[]
				{
					new[] { true, true },
					new[] { false, true },
					new[] { false, true }
				},
				new[]
				{
					new[] { false, false, true },
					new[] { true, true, true }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> LAntiClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, true },
					new[] { false, true, false },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, true },
					new[] { false, false, true }
				},
				new[]
				{
					new[] { false, true, true },
					new[] { false, true, false },
					new[] { false, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { true, true }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, true },
					new[] { false, false, true }
				}
			},

			{
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, true }
				},
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { true, true }
				}
			}
		};
	}
}