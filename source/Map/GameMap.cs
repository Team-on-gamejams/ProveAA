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
		MapCell[,] map;

		public byte SizeY => (byte) map.GetLength(0);
		public byte SizeX => (byte) map.GetLength(1);

		GameMap(byte sizeX, byte sizeY) {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				map = new MapCell[sizeY, sizeX];
				for (byte i = 0; i < map.GetLength(0); ++i)
					for (byte j = 0; j < map.GetLength(1); ++j)
						map[i, j] = new MapCell();
			});
		}

		void RandomFill() {
			System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate {
				for (byte i = 0; i < map.GetLength(0); ++i) {
					for (byte j = 0; j < map.GetLength(1); ++j) {
						map[i, j].IsWall = true;
						map[i, j].IsInFog = true;
					}
				}

				for (byte i = 1; i < map.GetLength(0) - 1; ++i)
					map[i, 1].IsWall = false;
				for (byte i = 1; i < map.GetLength(1) - 1; ++i)
					map[map.GetLength(0) - 2, i].IsWall = false;

				var window = Game.Singletones.gameWindow;
				window.MazeGrid.RowDefinitions.Clear();
				for (byte i = 0; i < SizeY; ++i)
					window.MazeGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());
				window.MazeGrid.ColumnDefinitions.Clear();
				for (byte i = 0; i < SizeX; ++i)
					window.MazeGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());

				for (int i = 0; i < SizeY; ++i) {
					for (int j = 0; j < SizeX; ++j) {
						Grid.SetColumn(map[i, j].image, i);
						Grid.SetRow(map[i, j].image, j);
						window.MazeGrid.Children.Add(map[i, j].image);
					}
				}
			});
		}

		public static GameMap CreateMap(Creature.Player player) {
			player.PosY = player.PosX = 1;
			GameMap map = new Map.GameMap(Game.Settings.map_sizeX, Game.Settings.map_sizeY);
			Game.Singletones.game.Map = map;
			map.RandomFill();
			for (byte i = 0; i < map.SizeY; ++i) 
				for (byte j = 0; j < map.SizeX; ++j)
					if (!map[i, j].IsWall) {
						player.PosY = i;
						player.PosX = j;
						goto Find_Player_Pos;
					}
			Find_Player_Pos:
			return map;
		}

		public MapCell this[byte a, byte b] { get => map[a, b]; set => map[a, b]=value; }
	}
}
