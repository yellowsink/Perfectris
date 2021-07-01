using System;
using Avalonia.Controls;

namespace Perfectris
{
	public static class TetrisLogic
	{
		public static bool IsRenderNecessary(GameLoop<GameState> loop)
		{
			return false;
		}

		public static void Render(GameLoop<GameState> loop, Func<Grid> getGrid)
		{
			
		}
	}
}