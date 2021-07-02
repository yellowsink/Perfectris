using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Perfectris.Core;
using Perfectris.Core.Logic;

namespace Perfectris
{
	public partial class MainWindow : Window
	{
		private GameLoop<GameState> _gameLoop;
		private TetrisLogic         _logic = new();
		
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			_gameLoop = new(loop => _logic.Render(loop, SetGrid), _logic.IsRenderNecessary);
		}

		private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }

		public void SetGrid(TetrominoType?[][] grid) => throw new NotImplementedException();
	}
}