using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> TClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, true }
				},
				new[]
				{
					new[] { true, false },
					new[] { true, true },
					new[] { true, false }
				}
			},

			{
				new[]
				{
					new[] { true, false },
					new[] { true, true },
					new[] { true, false }
				},
				new[]
				{
					new[] { true, true, true },
					new[] { false, true, false }
				}
			},

			{
				new[]
				{
					new[] { true, true, true },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, true },
					new[] { true, true },
					new[] { false, true }
				}
			},

			{
				new[]
				{
					new[] { false, true },
					new[] { true, true },
					new[] { false, true }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, true }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> TAntiClockwise = new()
		{
			{
				new[]
				{
					new[] { true, false },
					new[] { true, true },
					new[] { true, false }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, true }
				}
			},

			{
				new[]
				{
					new[] { true, true, true },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { true, false },
					new[] { true, true },
					new[] { true, false }
				}
			},

			{
				new[]
				{
					new[] { false, true },
					new[] { true, true },
					new[] { false, true }
				},
				new[]
				{
					new[] { true, true, true },
					new[] { false, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, true }
				},
				new[]
				{
					new[] { false, true },
					new[] { true, true },
					new[] { false, true }
				}
			}
		};
	}
}