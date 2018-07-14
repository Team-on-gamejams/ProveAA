using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map {
	class GlobalGameMap {
		byte playerPosX, playerPosY;
		GlobalGameMapCell[,] globalMap;

		public GlobalGameMap() {
			globalMap = new GlobalGameMapCell[7, 4];
		}
	}
}
