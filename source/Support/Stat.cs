using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Stat {
		public short Base;
		public short Current;

		public override string ToString() {
			return $"{Current} ({Base})";
		}
	}
}
