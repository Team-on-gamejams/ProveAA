using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map {
	class GameCell {
		bool isSolid, isWall, isDoor, isInFog;

		public bool IsSolid { get => isSolid; set => isSolid = value; }
		public bool IsWall { get => isWall; set => isWall = value; }
		public bool IsDoor { get => isDoor; set => isDoor = value; }
		public bool IsInFog { get => isInFog; set => isInFog = value; }

		public GameCell() {
			isSolid = isWall = isInFog = true;
			isDoor = false;
		}
	}
}
