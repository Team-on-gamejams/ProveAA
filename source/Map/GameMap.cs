using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map {
	class GameMap {
		MapCell[,] map;

		public byte SizeY => (byte) map.GetLength(0);
		public byte SizeX => (byte) map.GetLength(1);

		public GameMap(byte sizeX, byte sizeY) {
			map = new MapCell[sizeY, sizeX];
			for (int i = 0; i < map.GetLength(0); ++i)
				for (int j = 0; j < map.GetLength(1); ++j) 
					map[i, j] = new MapCell();
		}

		public void RandomFill() {

		}

	}
}
