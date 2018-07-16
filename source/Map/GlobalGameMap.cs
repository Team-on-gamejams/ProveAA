using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map {
	class GlobalGameMap {
		byte playerPosX = 1, playerPosY = 3;
		GlobalGameMapCell[,] globalMap;

		public GlobalGameMap() {
			globalMap = new GlobalGameMapCell[7, 4];
			for (byte i = 0; i < globalMap.GetLength(0); ++i) {
				for (byte j = 0; j < globalMap.GetLength(1); ++j)
					globalMap[i, j] = new GlobalGameMapCell();
			}

			globalMap[3, 1].ChanceGenerator.Add(50);
			globalMap[3, 1].Generators.Add(new Map.Zone.MazeGenerator());
			globalMap[3, 1].ChanceGenerator.Add(50);
			globalMap[3, 1].Generators.Add(new Map.Zone.ForestGenerator());
		}

		public void RecreateLevel(GameMap map, Creature.Player player) {
			byte randPersent = Game.Rand.NextPersent();
			for(byte i = 0, currMinNum = 0; i < globalMap[playerPosY, playerPosX].ChanceGenerator.Count; ++i) {
				currMinNum += globalMap[playerPosY, playerPosX].ChanceGenerator[i];
				if(randPersent < currMinNum) {
					globalMap[playerPosY, playerPosX].Generators[i].GenerateMap(map, player);
					break;
				}
			}
		}

		public bool CanMoveUp => globalMap[playerPosY, playerPosX].IsOpenTop && playerPosY != 0;
		public bool CanMoveDown => globalMap[playerPosY, playerPosX].IsOpenBottom && playerPosY != globalMap.GetLength(0);
		public bool CanMoveLeft => globalMap[playerPosY, playerPosX].IsOpenRight && playerPosX != 0;
		public bool CanMoveRight => globalMap[playerPosY, playerPosX].IsOpenRight && playerPosX != globalMap.GetLength(1);

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
