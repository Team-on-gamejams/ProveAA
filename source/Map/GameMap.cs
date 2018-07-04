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
		static byte generation = 0;
		static List<Zone.BasicZoneGenerator> generators = new List<Zone.BasicZoneGenerator>() {
			new Zone.ZoneTest(),
			new Zone.FirstZone(),
		};
		GameCell[,] map;

		public byte SizeY => (byte)map.GetLength(0);
		public byte SizeX => (byte)map.GetLength(1);

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
			ClearMap();
			RandomFill(player);
		}

		public void OutputMap(Windows.GameWindow window) {
			for (byte i = 0; i < map.GetLength(0); ++i)
				for (byte j = 0; j < map.GetLength(1); ++j) {
					Grid.SetRow(map[i, j].imageCell, i);
					Grid.SetColumn(map[i, j].imageCell, j);
					Grid.SetZIndex(map[i, j].imageCell, 0);
					window.MazeGrid.Children.Add(map[i, j].imageCell);

					Grid.SetRow(map[i, j].imageContent, i);
					Grid.SetColumn(map[i, j].imageContent, j);
					Grid.SetZIndex(map[i, j].imageContent, 1);
					window.MazeGrid.Children.Add(map[i, j].imageContent);

					Grid.SetRow(map[i, j].imageLetter, i);
					Grid.SetColumn(map[i, j].imageLetter, j);
					Grid.SetZIndex(map[i, j].imageLetter, 2);
					map[i, j].imageLetter.HorizontalAlignment = HorizontalAlignment.Left;
					map[i, j].imageLetter.VerticalAlignment = VerticalAlignment.Top;
					window.MazeGrid.Children.Add(map[i, j].imageLetter);
				}
		}

		void ClearMap() {
			for (byte i = 0; i < map.GetLength(0); ++i)
				for (byte j = 0; j < map.GetLength(1); ++j)
					map[i, j].RefillValue();
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j)
					map[i, j].IsSolid = map[i, j].IsWall = false;
		}

		void RandomFill(Creature.Player player) {
			generators[generation].GenerateMap(this);
			generators[generation].PlaceMonster(this);
			generators[generation].PlaceItems(this);
			generators[generation].PlacePlayer(this, player);
			if (generation != generators.Count - 1)
				++generation;
		}

		public GameCell this[byte a, byte b] {
			get {
				if (a < 0 || a >= SizeY || b < 0 || b >= SizeX)
					return null;
				return map[a, b];
			}
			set => map[a, b] = value;
		}
	}
}

