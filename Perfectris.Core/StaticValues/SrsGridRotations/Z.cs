using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> ZClockwise = new()
		{
			{
				new[]
				{
					new[] { true, true, false },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, false, true },
					new[] { false, true, true },
					new[] { false, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, false, true },
					new[] { false, true, true },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, false },
					new[] { false, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, false },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, false },
					new[] { true, false, false }
				}
			},

			{
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, false },
					new[] { true, false, false }
				},
				new[]
				{
					new[] { true, true, false },
					new[] { false, true, true }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> ZAntiClockwise = new()
		{
			{
				new[]
				{
					new[] { false, false, true },
					new[] { false, true, true },
					new[] { false, true, false }
				},
				new[]
				{
					new[] { true, true, false },
					new[] { false, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, false },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, false, true },
					new[] { false, true, true },
					new[] { false, true, false }
				}
			},

			{
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, false },
					new[] { true, false, false }
				},
				new[]
				{
					new[] { false, false, false },
					new[] { true, true, false },
					new[] { false, true, true }
				}
			},

			{
				new[]
				{
					new[] { true, true, false },
					new[] { false, true, true }
				},
				new[]
				{
					new[] { false, true, false },
					new[] { true, true, false },
					new[] { true, false, false }
				}
			}
		};
	}
}