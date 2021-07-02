using System;

namespace Perfectris.Core.Logic
{
	public class TopOutHandler
	{
		private byte _types;

		public TopOutType[] TopOutTypes
		{
			get => _types.TopOutTypes();
			set => _types = value.ToByte();
		}

		public bool WillTopOut(TopOutCausingActionType action, bool[][] stack, Tetromino nextPiece, int intendedRotation = 0, int garbageRowsAdded = 0)
		{
			switch (action)
			{
				case TopOutCausingActionType.Spawn:
					
					break;
				case TopOutCausingActionType.Rotation:
					break;
				case TopOutCausingActionType.Garbage:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(action), action, "Not a valid top out causing action");
			}
			return false;
		}
	}

	public enum TopOutCausingActionType
	{
		Spawn,
		Rotation,
		Garbage
	}
}