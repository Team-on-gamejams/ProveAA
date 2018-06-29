using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map {
	class GameMap {
		GameCell[,] map;

		public GameMap() {
			map = new GameCell[Game.Settings.map_sizeY, Game.Settings.map_sizeX];
			for (byte i = 0; i < map.GetLength(0); ++i)
				for (byte j = 0; j < map.GetLength(1); ++j)
					map[i, j] = new GameCell();
		}

		public void NewLevel(Creature.BasicCreature player) {
			RandomFill();
			PlacePlayer(player);
		}

		void RandomFill() {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j)
					map[i, j].IsSolid = map[i, j].IsWall = false;
		}

		void PlacePlayer(Creature.BasicCreature player) {
			for (byte i = 1; i < map.GetLength(0) - 1; ++i)
				for (byte j = 1; j < map.GetLength(1) - 1; ++j) {
					if(!map[i, j].IsSolid) {
						player.PosX = j;
						player.PosY = i;
						return;
					}
				}
		}
	}
}
