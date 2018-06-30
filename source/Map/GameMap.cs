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
					window.MazeGrid.Children.Add(map[i, j].imageCell);
					Grid.SetRow(map[i, j].imageCell, i);
					Grid.SetColumn(map[i, j].imageCell, j);
					Grid.SetZIndex(map[i, j].imageCell, 0);
					window.MazeGrid.Children.Add(map[i, j].imageContent);
					Grid.SetRow(map[i, j].imageContent, i);
					Grid.SetColumn(map[i, j].imageContent, j);
					Grid.SetZIndex(map[i, j].imageContent, 1);
				}
		}

		void RandomFill() {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j)
					map[i, j].IsSolid = map[i, j].IsWall = false;

			for (byte i = 1; i < map.GetLength(1) - 1; ++i)
				map[2, i].IsSolid = map[2, i].IsWall = true;
			map[2, map.GetLength(1) - 2].IsWall = false;
			map[2, map.GetLength(1) - 2].IsDoor = true;
			//map[2, map.GetLength(1) - 2].IsSolid = false;

			map[1, map.GetLength(1) - 2].CellContent = new Card.Card(new Item.Potion.HealingPotion());
			map[1, map.GetLength(1) - 3].CellContent = new Card.Card(new Item.Potion.ManaPotion());
			map[1, map.GetLength(1) - 4].CellContent = new Card.Card(new Item.Potion.RefreshPotion());
		}

		void PlacePlayer(Creature.Player player) {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j) {
					if(!map[i, j].IsSolid) {
						player.PosX = j;
						player.PosY = i;
						player.PosChanged();
						return;
					}
				}
		}

		public GameCell this[byte a, byte b] { get => map[a, b]; set => map[a, b] = value; }
	}
}

