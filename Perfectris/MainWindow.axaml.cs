using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Perfectris
{
	public partial class MainWindow : Window
	{
		private GameLoop<GameState> _gameLoop;
		
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			_gameLoop = new(loop => TetrisLogic.Render(loop, GetGrid), TetrisLogic.IsRenderNecessary);
		}

		private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }

		public Grid GetGrid() => throw new NotImplementedException();
	}
}