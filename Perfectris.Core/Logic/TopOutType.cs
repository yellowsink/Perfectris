using System.Linq;

namespace Perfectris.Core.Logic
{
	public static class TopOutTypeHelpers
	{
		public static bool IsTopOutTypeEnabled(this byte topOutTypes, TopOutType type) => (topOutTypes & (byte) type) != 0;
		public static byte EnableTopOutType(this byte    topOutTypes, TopOutType type) => (byte) (topOutTypes | (byte) type);
		public static byte DisableTopOutType(this byte   topOutTypes, TopOutType type) => (byte) (topOutTypes & (byte) ~type);

		public static TopOutType[] TopOutTypes(this byte topOutTypes)
		{
			var allTypes = new[]
			{
				TopOutType.BlockOut, TopOutType.LockOut, TopOutType.PartialLockOut,
				TopOutType.GarbageOut, TopOutType.TopOut
			};
			
			return allTypes.Where(type => IsTopOutTypeEnabled(topOutTypes, type)).ToArray();
		}

		public static byte ToByte(this TopOutType[] types)
			=> (byte) types.Aggregate(0, (current, type) => (byte) (current | (byte) type));
	}
	
	public enum TopOutType
	{
		BlockOut       = 0b00000001,
		LockOut        = 0b00000010,
		PartialLockOut = 0b00000100,
		GarbageOut     = 0b00001000,
		TopOut         = 0b00010000
	}
}