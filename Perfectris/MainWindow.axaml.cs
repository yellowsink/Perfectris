using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Perfectris.Core;
using Perfectris.Core.Enums;
using Perfectris.Core.Logic;
using Perfectris.Core.Types;

namespace Perfectris
{
	public partial class MainWindow : Window
	{
		private GameLoop<GameStateWrapper> _gameLoop;
		private TetrisLogic         _logic = new();
		
		public MainWindow()
		{
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif
			_gameLoop = new(loop => _logic.Update(loop, SetGrid));
		}

		private void InitializeComponent() { AvaloniaXamlLoader.Load(this); }

		public void SetGrid(TetrominoType?[][] grid) => throw new NotImplementedException();
	}
}