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
		static char lastZone = 'a';
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
			RandomFill();
			PlacePlayer(player);
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
		}

		void RandomFill() {
			GenerateWalls();
			PlaceDoors();
			PlaceItems();

			void GenerateWalls() {
				for (byte i = 1; i < map.GetLength(0) - 1; ++i)
					for (byte j = 1; j < map.GetLength(1) - 1; ++j)
						map[i, j].IsSolid = map[i, j].IsWall = false;


				for (byte i = 1; i < map.GetLength(1) - 1; ++i)
					map[2, i].IsSolid = map[2, i].IsWall = true;
			}

			void PlaceDoors() {
				List<Tuple<byte, byte>> doorPos = new List<Tuple<byte, byte>>();
				doorPos.Add(new Tuple<byte, byte>(2, (byte)(map.GetLength(1) - 2)));
				doorPos.Add(new Tuple<byte, byte>((byte)(map.GetLength(0) - 1), 1));

				foreach (var i in doorPos) {
					SetZoneLetters((byte)(i.Item1-1), i.Item2, lastZone);
					map[i.Item1, i.Item2].IsWall = false;
					map[i.Item1, i.Item2].IsDoor = true;
					map[i.Item1, i.Item2].CellZone = lastZone;
					map[i.Item1-1, i.Item2].CellContent = new Card.Card(new Spell.Move.OpenDoor(lastZone));
					++lastZone;
				}
			}

			void PlaceItems() {
				map[1, map.GetLength(1) - 3].CellContent = new Card.Card(new Item.Potion.ManaPotion());
			}
		}

		void PlacePlayer(Creature.Player player) {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j) {
					if (!map[i, j].IsSolid) {
						player.PosX = j;
						player.PosY = i;
						player.PosChanged();
						return;
					}
				}
		}

		void SetZoneLetters(byte i, byte j, char letter) {
			if (map[i, j].CellZone == 0 && !map[i, j].IsSolid) {
				map[i, j].CellZone = letter;
				SetZoneLetters(i, (byte)(j + 1), letter);
				SetZoneLetters(i, (byte)(j - 1), letter);
				SetZoneLetters((byte)(i + 1), j, letter);
				SetZoneLetters((byte)(i - 1), j, letter);
			}
		}

		public GameCell this[byte a, byte b] { get => map[a, b]; set => map[a, b] = value; }
	}
}

