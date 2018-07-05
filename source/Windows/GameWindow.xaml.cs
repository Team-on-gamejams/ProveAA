﻿using System;
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
			Card.Card.window = this;
		}

		private void Window_Loaded(object sender, EventArgs e) {
			ResizeUI();
			this.SizeChanged += (a, b) => ResizeUI();
			this.StateChanged += (a, b) => {
				System.Timers.Timer t = new System.Timers.Timer {
					Interval = 100,
					AutoReset = false,
					Enabled = false,
				};
				t.Elapsed += (c, d) => { ResizeUI(); t.Enabled = false; };
				t.Enabled = true;
			};

			Game.Game game = new Game.Game(this);
			game.Start(this);
		}

		private double hashedCardSizeMod = 1.5 / Settings.handSize;
		void ResizeUI() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				CardsGrid.Height = this.RenderSize.Width * hashedCardSizeMod;
				double mazeGridSize = Math.Min(CenterGrid.RenderSize.Height, CenterGrid.RenderSize.Width);
				MazeGrid.Width = MazeGrid.Height = mazeGridSize;
			});
		}

	}
}
