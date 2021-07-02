using System.Collections.Generic;
using Perfectris.Core.Logic;

// ReSharper disable once CheckNamespace
namespace Perfectris.Core.StaticValues
{
	public partial class SrsGridRotations
	{
		public static Dictionary<bool[][], bool[][]> Clockwise
			=> IClockwise
			  .Merge(JClockwise)
			  .Merge(LClockwise)
			  .Merge(OClockwise)
			  .Merge(SClockwise)
			  .Merge(TClockwise)
			  .Merge(ZClockwise);

		public static Dictionary<bool[][], bool[][]> Anticlockwise
			=> IAntiClockwise
			  .Merge(JAntiClockwise)
			  .Merge(LAntiClockwise)
			  .Merge(OAntiClockwise)
			  .Merge(SAntiClockwise)
			  .Merge(TAntiClockwise)
			  .Merge(ZAntiClockwise);
	}
}