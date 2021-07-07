namespace Perfectris.Core.Types
{
	public class InputState
	{
		public bool          MoveLeftHeld  { get; private set; }
		public bool          MoveRightHeld { get; private set; }
		public bool          SoftDropHeld  { get; private set; }
		public MoveDirection MoveDirection { get; private set; } = MoveDirection.None;
		public int           PressDownTick { get; private set; } = -1;

		public void MoveLeftDown(int tick)
		{
			bool? moveLeftHeld = MoveLeftHeld;
			Down(ref moveLeftHeld, MoveDirection.Left, tick);
			MoveLeftHeld = moveLeftHeld!.Value;
		}

		public void MoveLeftUp()
		{
			bool? moveLeftHeld = MoveLeftHeld;
			Up(ref moveLeftHeld, MoveDirection.Left);
			MoveLeftHeld = moveLeftHeld!.Value;
		}

		public void MoveRightDown(int tick)
		{
			bool? moveRightHeld = MoveRightHeld;
			Down(ref moveRightHeld, MoveDirection.Right, tick);
			MoveRightHeld = moveRightHeld!.Value;
		}

		public void MoveRightUp()
		{
			bool? moveRightHeld = MoveRightHeld;
			Up(ref moveRightHeld, MoveDirection.Right);
			MoveRightHeld = moveRightHeld!.Value;
		}

		public void SoftDropDown(int tick)
		{
			bool? softDropHeld = SoftDropHeld;
			Down(ref softDropHeld, null, tick);
			SoftDropHeld = softDropHeld!.Value;
		}

		public void SoftDropUp()
		{
			bool? softDropHeld = SoftDropHeld;
			Up(ref softDropHeld, null);
			SoftDropHeld = softDropHeld!.Value;
		}
		
		
		private void Down(ref bool? keyHeld, MoveDirection? direction, int tick)
		{
			keyHeld       = true;
			MoveDirection = direction ?? MoveDirection;
			PressDownTick = tick;
		}

		private void Up(ref bool? keyHeld, MoveDirection? direction)
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