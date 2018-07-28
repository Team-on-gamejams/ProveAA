using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Map.Zone;

namespace ProveAA.Map {
	class GlobalGameMapCell {
		public string CellName { get; set; }
		public bool IsOpenLeft { get; set; }
		public bool IsOpenTop { get; set; }
		public bool IsOpenRight { get; set; }
		public bool IsOpenBottom { get; set; }

		public List<byte> ChanceGenerator { get; private set; }
		public List<BasicZoneGenerator> Generators { get; private set; }

		public void AddZone(BasicZoneGenerator zoneGenerator, byte chance) {
			ChanceGenerator.Add(chance);
			Generators.Add(zoneGenerator);
		}

		public void ClearZones() {
			ChanceGenerator.Clear();
			Generators.Clear();
		}

		public GlobalGameMapCell() {
			Generators = new List<BasicZoneGenerator>(2);
			ChanceGenerator = new List<byte>(2);
		}
	}
}
