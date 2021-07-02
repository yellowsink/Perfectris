namespace Perfectris.Core
{
	public class InputState
	{
		public bool          MoveLeftHeld  { get; private set; }
		public bool          MoveRightHeld { get; private set; }
		public bool          SoftDropHeld  { get; private set; }
		public MoveDirection MoveDirection { get; private set; } = MoveDirection.None;

		public void MoveLeftDown()
		{
			bool? moveLeftHeld = MoveLeftHeld;
			MoveDown(ref moveLeftHeld, MoveDirection.Left);
			MoveLeftHeld = moveLeftHeld!.Value;
		}

		public void MoveLeftUp()
		{
			bool? moveLeftHeld = MoveLeftHeld;
			MoveUp(ref moveLeftHeld, MoveDirection.Left);
			MoveLeftHeld = moveLeftHeld!.Value;
		}

		public void MoveRightDown()
		{
			bool? moveRightHeld = MoveRightHeld;
			MoveDown(ref moveRightHeld, MoveDirection.Right);
			MoveRightHeld = moveRightHeld!.Value;
		}

		public void MoveRightUp()
		{
			bool? moveRightHeld = MoveRightHeld;
			MoveUp(ref moveRightHeld, MoveDirection.Right);
			MoveRightHeld = moveRightHeld!.Value;
		}

		public void SoftDropDown()
		{
			bool? softDropHeld = SoftDropHeld;
			MoveDown(ref softDropHeld, null);
			SoftDropHeld = softDropHeld!.Value;
		}

		public void SoftDropUp()
		{
			bool? softDropHeld = SoftDropHeld;
			MoveUp(ref softDropHeld, null);
			SoftDropHeld = softDropHeld!.Value;
		}
		
		
		private void MoveDown(ref bool? keyHeld, MoveDirection? direction)
		{
			keyHeld       = true;
			MoveDirection = direction ?? MoveDirection;
		}

		private void MoveUp(ref bool? keyHeld, MoveDirection? direction)
		{
			keyHeld = false;
			if (MoveDirection == direction) MoveDirection = MoveDirection.None;
		}
	}

	public enum MoveDirection
	{
		None,
		Left,
		Right
	}
}