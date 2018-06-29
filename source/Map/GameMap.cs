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

namespace ProveAA.Map {
	class GameMap {
		GameCell[,] map;

		public GameMap(Windows.GameWindow window) {
			map = new GameCell[Game.Settings.map_sizeY, Game.Settings.map_sizeX];
			for (byte i = 0; i < map.GetLength(0); ++i) {
				window.MazeGrid.RowDefinitions.Add(new RowDefinition());
				for (byte j = 0; j < map.GetLength(1); ++j) {
					map[i, j] = new GameCell();
				}
			}
			for (byte j = 0; j < map.GetLength(1); ++j)
				window.MazeGrid.ColumnDefinitions.Add(new ColumnDefinition());

		}

		public void NewLevel(Creature.Player player) {
			RandomFill();
			PlacePlayer(player);
		}

		public void OutputMap(Windows.GameWindow window) {
			for (byte i = 0; i < map.GetLength(0); ++i)
				for (byte j = 0; j < map.GetLength(1); ++j) {
					window.MazeGrid.Children.Add(map[i, j].image);
					Grid.SetRow(map[i, j].image, i);
					Grid.SetColumn(map[i, j].image, j);
					Grid.SetZIndex(map[i, j].image, 0);
				}
		}

		void RandomFill() {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j)
					map[i, j].IsSolid = map[i, j].IsWall = false;
		}

		void PlacePlayer(Creature.Player player) {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j) {
					if(!map[i, j].IsSolid) {
						player.PosX = j;
						player.PosY = i;
						return;
					}
				}
		}

		public GameCell this[byte a, byte b] { get => map[a, b]; set => map[a, b] = value; }
	}
}

