using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Map.Zone {
	abstract class BasicZoneGenerator {
		abstract public void GenerateMap(GameMap map, Creature.Player player);
	}
}
