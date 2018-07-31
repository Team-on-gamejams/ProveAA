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
	class GlobalGameMap {
		byte playerPosX = 1, playerPosY = 3;
		GlobalGameMapCell[,] globalMap;

		public GlobalGameMap() {
			globalMap = new GlobalGameMapCell[7, 4];
			for (byte i = 0; i < globalMap.GetLength(0); ++i) {
				for (byte j = 0; j < globalMap.GetLength(1); ++j) {
					globalMap[i, j] = new GlobalGameMapCell();
					globalMap[i, j].AddZone(new Map.Zone.MazeGenerator(), 100);
				}
			}

			for (byte i = 0; i < globalMap.GetLength(0); ++i) {
				for (byte j = 0; j < globalMap.GetLength(1); ++j)
					globalMap[i, j].IsOpenLeft = globalMap[i, j].IsOpenRight = true;
				globalMap[i, 0].IsOpenLeft = false;
				globalMap[i, globalMap.GetLength(1) - 1].IsOpenRight = false;
			}
			globalMap[1, 2].IsOpenRight = globalMap[1, 3].IsOpenLeft =  false;
			globalMap[2, 1].IsOpenRight = globalMap[2, 2].IsOpenLeft =  false;
			globalMap[2, 0].IsOpenRight = globalMap[2, 1].IsOpenLeft = false;

			globalMap[0, 2].IsOpenBottom = globalMap[1, 2].IsOpenTop = true;
			globalMap[5, 2].IsOpenBottom = globalMap[6, 2].IsOpenTop = true;

			globalMap[1, 0].IsOpenBottom = globalMap[2, 0].IsOpenTop = true;
			globalMap[2, 0].IsOpenBottom = globalMap[3, 0].IsOpenTop = true;
			globalMap[3, 0].IsOpenBottom = globalMap[4, 0].IsOpenTop = true;
			globalMap[4, 0].IsOpenBottom = globalMap[5, 0].IsOpenTop = true;

			globalMap[1, 3].IsOpenBottom = globalMap[2, 3].IsOpenTop = true;
			globalMap[2, 3].IsOpenBottom = globalMap[3, 3].IsOpenTop = true;
			globalMap[3, 3].IsOpenBottom = globalMap[4, 3].IsOpenTop = true;

			globalMap[2, 1].IsOpenBottom = globalMap[3, 1].IsOpenTop = true;

			globalMap[3, 1].ClearZones();
			globalMap[3, 1].AddZone(new Map.Zone.MazeGenerator(),   50);
			globalMap[3, 1].AddZone(new Map.Zone.ForestGenerator(), 50);
		}

		public void RecreateLevel(GameMap map, Creature.Player player) {
			REPEAT_CREATION:
			try {
				byte randPersent = Game.Rand.NextPersent();
				for (byte i = 0, currMinNum = 0; i < globalMap[playerPosY, playerPosX].ChanceGenerator.Count; ++i) {
					currMinNum += globalMap[playerPosY, playerPosX].ChanceGenerator[i];
					if (randPersent < currMinNum) {
						globalMap[playerPosY, playerPosX].Generators[i].GenerateMap(map, player);
						break;
					}
				}
			}
			catch (Exception ex) {
				MessageBox.Show(ex.Message + "\n\n\n\n Write to developer about it, pls)\nPress OK for new attempt", "Error in creation!", MessageBoxButton.OK, MessageBoxImage.Error);
				goto REPEAT_CREATION;
			}
		}

		public bool CanMoveUp => globalMap[playerPosY, playerPosX].IsOpenTop && playerPosY != 0;
		public bool CanMoveDown => globalMap[playerPosY, playerPosX].IsOpenBottom && playerPosY != globalMap.GetLength(0);
		public bool CanMoveLeft => globalMap[playerPosY, playerPosX].IsOpenLeft && playerPosX != 0;
		public bool CanMoveRight => globalMap[playerPosY, playerPosX].IsOpenRight && playerPosX != globalMap.GetLength(1);

		public GlobalGameMapCell LeftFromPlayer => globalMap[playerPosY, playerPosX - 1];
		public GlobalGameMapCell RightFromPlayer => globalMap[playerPosY, playerPosX + 1];
		public GlobalGameMapCell UpFromPlayer => globalMap[playerPosY - 1, playerPosX];
		public GlobalGameMapCell DownFromPlayer => globalMap[playerPosY + 2, playerPosX];

		public void SetMarkerPos() {
			Grid.SetRow(Support.DialogBox.window.PlayerMarker, playerPosY);
			Grid.SetColumn(Support.DialogBox.window.PlayerMarker, playerPosX);
		}

		public void MoveUp() {
			if (CanMoveUp)
				--playerPosY;	
		}

		public void MoveLeft() {
			if (CanMoveLeft)
				--playerPosX;
		}

		public void MoveRight() {
			if (CanMoveRight)
				++playerPosX;
		}

		public void MoveDown() {
			if (CanMoveDown)
				++playerPosY;
		}

	}
}
