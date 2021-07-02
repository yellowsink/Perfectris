using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> SClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, true },
					new[] { true, true, false }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, true },
					new[] { false, false, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, true },
					new[] { false, false, true }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { false, true, true },
					new[] { true, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { false, true, true },
					new[] { true, true, false }
				},
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, false },
					new[] { false, true, false }
				}
			},

			{
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, false },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { false, true, true },
					new[] { true, true, false }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> SAntiClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, true },
					new[] { false, false, true }
				},
				new[]
				{
					new[] { false, true, true },
					new[] { true, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { false, true, true },
					new[] { true, true, false }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { false, true, true },
					new[] { false, false, true }
				}
			},

			{
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, false },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { false, true, true },
					new[] { true, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, true, true },
					new[] { true, true, false }
				},
				new[]
				{
					new[] { true, false, false },
					new[] { true, true, false },
					new[] { false, true, false }
				}
			}
		};
	}
}