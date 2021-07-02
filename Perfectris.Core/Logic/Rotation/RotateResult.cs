namespace Perfectris.Core.Logic.Rotation
{
	public class RotateResult
	{
		/// <summary>
		/// The new piece
		/// </summary>
		public bool[][] NewGrid;

		/// <summary>
		/// Required horizontal movement for wall kicks
		/// </summary>
		public int TranslateX;
		/// <summary>
		/// Required vertical movement for floor kicks
		/// </summary>
		public int TranslateY;
	}
}