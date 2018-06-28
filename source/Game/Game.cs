using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProveAA.Map;
using ProveAA.Creature;

namespace ProveAA.Game {
	class Game {
		Windows.GameWindow window;
		Player player;
		Map.GameMap map;

		public void Start(ProveAA.Windows.GameWindow window) {
			InitGame();
			this.window = window;
			InitWindow();
		}

		void InitWindow() {
			for(int i = 0; i < map.SizeY; ++i)
				window.MazeGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());
			for (int i = 0; i < map.SizeX; ++i)
				window.MazeGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
		}

		void InitGame() {
			map = new Map.GameMap(Settings.map_sizeX, Settings.map_sizeY);
			map.RandomFill();
			player = new Player();
		}
	}
}
