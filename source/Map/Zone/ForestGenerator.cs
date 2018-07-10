using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	class ForestGenerator : MazeGenerator {
		public ForestGenerator() {
			this.generatorZoneStyleNum = 1;
		}

		public override void GenerateMap(GameMap map, Creature.Player player) {
			base.GenerateMap(map, player);
		}
	}
}
