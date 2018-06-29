using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ProveAA.Game;

namespace ProveAA.Windows {
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window {
		public GameWindow() {
			InitializeComponent();
		}

		private void Window_Loaded(object sender, EventArgs e) {
			this.SizeChanged += (a, b) => ResizeUI();
			this.StateChanged += (a, b) => ResizeUI();

			System.Timers.Timer t = new System.Timers.Timer {
				AutoReset = false,
				Interval = 500,
			};
			t.Elapsed += (a, b) => {Singletones.game.Start(); t.Stop(); };
			t.Start();
		}

		private double hashedCardSizeMod = 1.5 / 7;
		void ResizeUI() {
			CardsGrid.Height = this.RenderSize.Width * hashedCardSizeMod;
			double mazeGridSize = Math.Min(CenterGrid.RenderSize.Height, CenterGrid.RenderSize.Width);
			MazeGrid.Width = MazeGrid.Height = mazeGridSize;
		}

	}
}
