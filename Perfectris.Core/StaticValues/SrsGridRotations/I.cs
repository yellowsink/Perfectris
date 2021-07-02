using System.Collections.Generic;
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public static partial class SrsGridRotations
	{
		private static readonly Dictionary<bool[][], bool[][]> IClockwise = new()
		{
			{
				new[]
				{
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				},
				new[]
				{
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true }
				}
			},

			{
				new[]
				{
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true }
				},
				new[]
				{
					new[] { false, true, false, false },
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false, false },
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				},
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { false, true },
					new[] { false, true }
				}
			},

			{
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { false, true },
					new[] { false, true }
				},
				new[]
				{
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				}
			}
		};

		private static readonly Dictionary<bool[][], bool[][]> IAntiClockwise = new()
		{
			{
				new[]
				{
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true }
				},
				new[]
				{
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false, false },
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				},
				new[]
				{
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true },
					new[] { false, false, true }
				}
			},

			{
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { false, true },
					new[] { false, true }
				},
				new[]
				{
					new[] { false, true, false, false },
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				}
			},

			{
				new[]
				{
					new[] { false, true, false, false },
					new[] { true, true, true, true }
				},
				new[]
				{
					new[] { false, true },
					new[] { false, true },
					new[] { false, true },
					new[] { false, true }
				}
			}
		};
	}
}