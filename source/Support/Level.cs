using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProveAA.Support {
	class Level {
		public byte CurrentLvl, CurrentExp, ExpToNext;

		public override string ToString() {
			return $"{CurrentExp} / {ExpToNext}  ({CurrentLvl})";
		}
	}
}
