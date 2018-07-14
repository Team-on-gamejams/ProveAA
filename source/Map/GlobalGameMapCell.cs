using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProveAA.Map.Zone;

namespace ProveAA.Map {
	class GlobalGameMapCell {
		public bool IsOpenLeft { get; set; }
		public bool IsOpenTop { get; set; }
		public bool IsOpenRight { get; set; }
		public bool IsOpenBottom { get; set; }

		public List<byte> ChanceGenerator { get; private set; }
		internal List<BasicZoneGenerator> Generators { get; private set; }

		public GlobalGameMapCell() {
			Generators = new List<BasicZoneGenerator>(2);
			ChanceGenerator = new List<byte>(2);
		}

	}
}
